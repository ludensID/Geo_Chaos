using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public class EcsEntities
  {
    private readonly List<IEcsPredicate> _predicates = new List<IEcsPredicate>();
    private readonly List<IEcsPredicate> _tempPredicates = new List<IEcsPredicate>();
    private readonly EcsFilter _filter;
    private readonly EcsWorld _world;
    private readonly EcsEntity _cachedEntity;
    private readonly InvokePredicateClosure _invokePredicateClosure;

    public EcsFilter Filter => _filter;
    public EcsWorld World => _world;

    public EcsEntities(EcsFilter filter)
    {
      _filter = filter;
      _world = filter.GetWorld();
      _cachedEntity = new EcsEntity(_world);
      _invokePredicateClosure = new InvokePredicateClosure();
    }

    public Enumerator GetEnumerator()
    {
      return new Enumerator(this);
    }

    public bool Any()
    {
      using Enumerator enumerator = GetEnumerator();
      return enumerator.MoveNext();
    }

    public IEnumerable<EcsEntity> ToEnumerable()
    {
      using Enumerator enumerator = GetEnumerator();
      while (enumerator.MoveNext())
        yield return enumerator.Current;
    }

    public EcsEntities Clone()
    {
      var instance = new EcsEntities(_filter);
      instance._predicates.AddRange(_predicates);
      return instance;
    }

    public EcsEntities Check<TComponent>(Predicate<TComponent> predicate) where TComponent : struct, IEcsComponent
    {
      EcsPredicate<TComponent> ecsPredicate = EcsPredicatePool.PopPredicate<TComponent>();
      ecsPredicate.Predicate = predicate;
      ecsPredicate.Pool = World.GetPool<TComponent>();
      _tempPredicates.Add(ecsPredicate);
      return this;
    }

    public EcsEntities Where<TComponent>(Predicate<TComponent> predicate) where TComponent : struct, IEcsComponent
    {
      _predicates.Add(new EcsPredicate<TComponent>(predicate, World.GetPool<TComponent>()));
      return this;
    }

    public class InvokePredicateClosure : EcsClosure<IEcsPredicate>
    {
      public int I;

      public Predicate<IEcsPredicate> SpecifyPredicate(int i)
      {
        I = i;
        return Predicate;
      }

      protected override bool Call(IEcsPredicate predicate)
      {
        return predicate.Invoke(I);
      }
    }

    public struct Enumerator : IDisposable
    {
      private readonly EcsEntities _entities;
      private EcsFilter.Enumerator _enumerator;

      public Enumerator(EcsEntities entities)
      {
        _entities = entities;
        _enumerator = entities._filter.GetEnumerator();
      }

      public EcsEntity Current
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
          _entities._cachedEntity.Entity = _enumerator.Current;
          return _entities._cachedEntity;
        }
      }

      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public bool MoveNext()
      {
        while (_enumerator.MoveNext())
        {
          _entities._invokePredicateClosure.I = _enumerator.Current;
          if (_entities._predicates.AllNonAlloc(_entities._invokePredicateClosure.Predicate)
            && _entities._tempPredicates.AllNonAlloc(_entities._invokePredicateClosure.Predicate))
          {
            return true;
          }
        }

        return false;
      }

      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public void Dispose()
      {
        _enumerator.Dispose();
        foreach (IEcsPredicate predicate in _entities._tempPredicates)
          EcsPredicatePool.PushPredicate(predicate);

        _entities._tempPredicates.Clear();
      }
    }
  }
}