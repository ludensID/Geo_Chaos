using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public class DeleteDragForceSystem : IEcsRunSystem
  {
    private readonly EcsWorld _physics;
    private readonly EcsWorld _game;
    private readonly EcsEntities _dragForces;

    public DeleteDragForceSystem(PhysicsWorldWrapper physicsWorldWrapper, GameWorldWrapper gameWorldWrapper)
    {
      _physics = physicsWorldWrapper.World;
      _game = gameWorldWrapper.World;

      _dragForces = _physics
        .Filter<DragForce>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity drag in _dragForces)
      {
        if (!drag.Get<Owner>().Entity.TryUnpackEntity(_game, out EcsEntity owner) || !owner.Has<DragForceAvailable>())
          drag.Dispose();
      }
    }
  }
}