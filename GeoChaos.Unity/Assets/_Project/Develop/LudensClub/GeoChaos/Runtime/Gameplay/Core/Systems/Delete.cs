using System;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class Delete<TComponent, TWrapper> : IEcsRunSystem where TComponent : struct, IEcsComponent
    where TWrapper : IEcsWorldWrapper
  {
    private readonly EcsWorld _world;
    private readonly EcsEntities _deletes;

    protected Delete(TWrapper wrapper, Action<EcsWorld.Mask> clarifier = null)
    {
      _world = wrapper.World;

      EcsWorld.Mask deleteMask = _world.Filter<TComponent>();
      clarifier?.Invoke(deleteMask);
      _deletes = deleteMask.Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity delete in _deletes)
        delete.Del<TComponent>();
    }
  }
  
  public class Delete<TComponent> : Delete<TComponent, GameWorldWrapper> where TComponent : struct, IEcsComponent
  {
    protected Delete(GameWorldWrapper gameWorldWrapper, Action<EcsWorld.Mask> clarifier = null) 
      : base(gameWorldWrapper, clarifier)
    {
    }
  }
}