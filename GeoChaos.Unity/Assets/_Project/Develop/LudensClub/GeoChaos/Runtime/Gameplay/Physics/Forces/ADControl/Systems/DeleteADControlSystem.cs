using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public class DeleteADControlSystem : IEcsRunSystem
  {
    private readonly EcsWorld _physics;
    private readonly EcsWorld _game;
    private readonly EcsEntities _controls;

    public DeleteADControlSystem(GameWorldWrapper gameWorldWrapper, PhysicsWorldWrapper physicsWorldWrapper)
    {
      _physics = physicsWorldWrapper.World;
      _game = gameWorldWrapper.World;

      _controls = _physics
        .Filter<ADControl>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity control in _controls)
      {
        EcsEntity owner = _game.UnpackEntity(control.Get<Owner>().Entity);
        if(!owner.IsAlive || !owner.Has<ADControllable>())
          control.Dispose();
      }
    }
  }
}