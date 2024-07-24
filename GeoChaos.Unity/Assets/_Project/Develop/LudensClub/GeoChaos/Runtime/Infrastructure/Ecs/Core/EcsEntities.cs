using System;
using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public class EcsEntities : IEnumerable<EcsEntity>
  {
    private readonly List<IEcsPredicate> _predicates = new List<IEcsPredicate>();
    private readonly EcsFilter _filter;
    private readonly EcsWorld _world;
    private readonly EcsEntity _cachedEntity;
    private readonly PredicateInvoker _invoker;

    public EcsWorld World => _world;

    public EcsEntities(EcsFilter filter)
    {
      _filter = filter;
      _world = filter.GetWorld();
      _cachedEntity = new EcsEntity { World = World };
      _invoker = new PredicateInvoker();
    }

    public IEnumerator<EcsEntity> GetEnumerator()
    {
      foreach (int i in _filter)
      {
        _invoker.I = i;
        if (_predicates.AllNonAlloc(_invoker))
        {
          _cachedEntity.Entity = i;
          yield return _cachedEntity;
        }
      }
    }
    
    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    public EcsEntities Where<TComponent>(Predicate<TComponent> predicate) where TComponent : struct, IEcsComponent
    {
      var entities = new EcsEntities(_filter);
      entities._predicates.AddRange(_predicates);
      entities._predicates.Add(new EcsPredicate<TComponent>(predicate, World.GetPool<TComponent>()));
      return entities;
    }

    private class PredicateInvoker : IPredicate<IEcsPredicate>
    {
      public int I;
      
      public bool Predicate(IEcsPredicate p)
      {
        return p.Invoke(I);
      }
    }
  }
}