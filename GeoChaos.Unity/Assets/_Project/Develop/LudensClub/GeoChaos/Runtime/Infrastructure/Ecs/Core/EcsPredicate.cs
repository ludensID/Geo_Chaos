using System;
using Leopotam.EcsLite;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public class EcsPredicate<TComponent> : IEcsPredicate where TComponent : struct, IEcsComponent
  {
    private readonly Predicate<TComponent> _predicate;
    private readonly EcsPool<TComponent> _pool;

    public EcsPredicate(Predicate<TComponent> predicate, EcsPool<TComponent> pool)
    {
      _predicate = predicate;
      _pool = pool;
    }
    
    public bool Invoke(int entity)
    {
      return _predicate.Invoke(_pool.Get(entity));
    }
  }
}