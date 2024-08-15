using System;
using Leopotam.EcsLite;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public class EcsPredicate<TComponent> : IEcsPredicate where TComponent : struct, IEcsComponent
  {
    public Type ComponentType { get; }
    public Predicate<TComponent> Predicate { get; set; }
    public EcsPool<TComponent> Pool { get; set; }

    public EcsPredicate()
    {
      ComponentType = typeof(TComponent);
    }

    public EcsPredicate(Predicate<TComponent> predicate, EcsPool<TComponent> pool)
    {
      Predicate = predicate;
      Pool = pool;
    }

    public bool Invoke(int entity)
    {
      return Predicate.Invoke(Pool.Get(entity));
    }
  }
}