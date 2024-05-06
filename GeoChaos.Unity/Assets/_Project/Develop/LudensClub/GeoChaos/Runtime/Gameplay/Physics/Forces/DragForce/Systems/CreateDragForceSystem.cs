using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public class CreateDragForceSystem : IEcsRunSystem
  {
    private readonly EcsWorld _physics;
    private readonly EcsEntities _dragForces;
    private readonly EcsWorld _game;
    private readonly EcsEntities _dragForcables;

    public CreateDragForceSystem(PhysicsWorldWrapper physicsWorldWrapper, GameWorldWrapper gameWorldWrapper)
    {
      _physics = physicsWorldWrapper.World;
      _game = gameWorldWrapper.World;

      _dragForces = _physics
        .Filter<DragForce>()
        .Collect();

      _dragForcables = _game
        .Filter<DragForceAvailable>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity forcable in _dragForcables)
      {
        bool hasDrag = false;
        foreach (EcsEntity _ in _dragForces
          .Where<Owner>(x => x.Entity.Equals(forcable.Pack())))
        {
          hasDrag = true;
        }

        if (!hasDrag)
        {
          _physics.CreateEntity()
            .Add<DragForce>()
            .Add((ref Owner owner) => owner.Entity = forcable.Pack())
            .Add<Gradient>()
            .Add<GradientRate>()
            .Add<RelativeSpeed>();
        }
      }
    }
  }
}