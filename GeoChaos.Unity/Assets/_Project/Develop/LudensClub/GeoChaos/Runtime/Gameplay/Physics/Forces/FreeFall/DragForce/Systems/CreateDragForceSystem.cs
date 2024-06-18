using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public class CreateDragForceSystem : IEcsRunSystem
  {
    private readonly IDragForceService _dragForceSvc;
    private readonly EcsWorld _physics;
    private readonly EcsEntities _dragForces;
    private readonly EcsWorld _game;
    private readonly EcsEntities _draggables;

    public CreateDragForceSystem(PhysicsWorldWrapper physicsWorldWrapper, GameWorldWrapper gameWorldWrapper, IDragForceService dragForceSvc)
    {
      _dragForceSvc = dragForceSvc;
      _physics = physicsWorldWrapper.World;
      _game = gameWorldWrapper.World;

      _draggables = _game
        .Filter<DragForceAvailable>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity draggable in _draggables)
      {
        if (_dragForceSvc.GetDragForce(draggable.Pack()) == null)
        {
          _physics.CreateEntity()
            .Add<FreeFall>()
            .Add<DragForce>()
            .Add((ref Owner owner) => owner.Entity = draggable.Pack())
            .Add<Gradient>()
            .Add<GradientRate>()
            .Add<RelativeSpeed>();
        }
      }
    }
  }
}