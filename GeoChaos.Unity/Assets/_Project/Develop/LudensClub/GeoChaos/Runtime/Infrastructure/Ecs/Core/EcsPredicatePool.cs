using System;
using System.Collections.Generic;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public static class EcsPredicatePool
  {
    private static readonly Dictionary<Type, List<PooledPredicate>>
      _predicates = new Dictionary<Type, List<PooledPredicate>>();

    private static readonly EcsPredicateFinder _ecsPredicateFinder = new EcsPredicateFinder();    
    private static readonly NotUsedPooledPredicateFinder _notUsedPooledPredicateFinder =
      new NotUsedPooledPredicateFinder();

    public static EcsPredicate<TComponent> PopPredicate<TComponent>() where TComponent : struct, IEcsComponent
    {
      Type type = typeof(TComponent);
      if (!_predicates.TryGetValue(type, out List<PooledPredicate> predicates))
      {
        predicates = new List<PooledPredicate>();
        _predicates.Add(type, predicates);
      }

      IEcsPredicate predicate;
      PooledPredicate pooledPredicate = predicates.FindNonAlloc(_notUsedPooledPredicateFinder);
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
        _ecsPredicateFinder.EcsPredicate = predicate;
        PooledPredicate poolPredicate = predicates.FindNonAlloc(_ecsPredicateFinder);
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

    private class NotUsedPooledPredicateFinder : IPredicate<PooledPredicate>
    {
      public bool Predicate(PooledPredicate obj)
      {
        return !obj.Used;
      }
    }
    
    private class EcsPredicateFinder : IPredicate<PooledPredicate>
    {
      public IEcsPredicate EcsPredicate;
        
      public bool Predicate(PooledPredicate obj)
      {
        return obj.Predicate == EcsPredicate;
      }
    }
  }
}