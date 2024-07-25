using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Move;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public class StartFallFreeSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsWorld _physics;
    private readonly EcsEntities _actionEvents;
    private readonly EcsEntities _prepares;

    public StartFallFreeSystem(GameWorldWrapper gameWorldWrapper, PhysicsWorldWrapper physicsWorldWrapper)
    {
      _game = gameWorldWrapper.World;
      _physics = physicsWorldWrapper.World;

      _actionEvents = _game
        .Filter<OnActionFinished>()
        .Collect();

      _prepares = _physics
        .Filter<Prepared>()
        .Exc<Delay>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity action in _actionEvents)
      {
        foreach (EcsEntity prepare in _prepares
          .Check<Owner>(x => x.Entity.EqualsTo(action.Pack())))
        {
          prepare
            .Del<Prepared>()
            .Add<Enabled>()
            .Change((ref Gradient gradient) => gradient.Value = 0);

          if (prepare.Has<ADControl>())
            action.Add<FreeRotating>();
        }

        action
          .Has<FreeFalling>(true)
          .Del<OnActionFinished>();
      }
    }
  }
}