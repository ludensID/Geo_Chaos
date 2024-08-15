using System;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class AddSystem<TAddComponent, TFilterComponent, TWorldWrapper> : IEcsRunSystem
    where TAddComponent : struct, IEcsComponent
    where TFilterComponent : struct, IEcsComponent
    where TWorldWrapper : IEcsWorldWrapper
  {
    private readonly EcsWorld _world;
    private readonly EcsEntities _addEntities;

    public AddSystem(TWorldWrapper worldWrapper, Action<EcsWorld.Mask> clarifier = null)
    {
      _world = worldWrapper.World;
      
      EcsWorld.Mask deleteMask = _world.Filter<TFilterComponent>();
      clarifier?.Invoke(deleteMask);
      _addEntities = deleteMask.Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity entity in _addEntities)
        entity.Add<TAddComponent>();
    }
  }
  
  public class AddSystem<TAddComponent, TFilterComponent> : AddSystem<TAddComponent, TFilterComponent, GameWorldWrapper>
    where TAddComponent : struct, IEcsComponent
    where TFilterComponent : struct, IEcsComponent
  {
    public AddSystem(GameWorldWrapper worldWrapper, Action<EcsWorld.Mask> clarifier = null) 
        : base(worldWrapper, clarifier)
    {
    }
  }
}