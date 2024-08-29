using System;
using System.Collections.Generic;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public static class EcsPredicatePool
  {
    private static readonly Dictionary<Type, List<PooledPredicate>>
      _predicates = new Dictionary<Type, List<PooledPredicate>>();

    private static readonly PooledPredicateClosure _isPooledPredicateEqualsClosure
      = new PooledPredicateClosure((poolPredicate, predicate) => poolPredicate.Predicate == predicate);

    private static readonly Predicate<PooledPredicate> _isNotUsedPooledPredicate = x => !x.Used;

    public static EcsPredicate<TComponent> PopPredicate<TComponent>() where TComponent : struct, IEcsComponent
    {
      Type type = typeof(TComponent);
      if (!_predicates.TryGetValue(type, out List<PooledPredicate> predicates))
      {
        predicates = new List<PooledPredicate>();
        _predicates.Add(type, predicates);
      }

      IEcsPredicate predicate;
      PooledPredicate pooledPredicate = predicates.Find(_isNotUsedPooledPredicate);
      if (pooledPredicate == null)
      {
        predicate = new EcsPredicate<TComponent>();
        pooledPredicate = new PooledPredicate(predicate);
        predicates.Add(pooledPredicate);
      }
      else
      {
        predicate = pooledPredicate.Predicate;
      }

      pooledPredicate.Used = true;

      return (EcsPredicate<TComponent>)predicate;
    }

    public static void PushPredicate(IEcsPredicate predicate)
    {
      if (_predicates.TryGetValue(predicate.ComponentType, out List<PooledPredicate> predicates))
      {
        PooledPredicate poolPredicate = predicates.Find(_isPooledPredicateEqualsClosure.SpecifyPredicate(predicate));
        if (poolPredicate != null)
          poolPredicate.Used = false;
      }
    }

    private class PooledPredicate
    {
      public bool Used;
      public IEcsPredicate Predicate;

      public PooledPredicate(IEcsPredicate predicate)
      {
        Predicate = predicate;
      }
    }

    private class PooledPredicateClosure : SpecifiedClosure<PooledPredicate, IEcsPredicate>
    {
      public PooledPredicateClosure(Func<PooledPredicate, IEcsPredicate, bool> predicate) : base(predicate)
      {
      }
    }
  }
}