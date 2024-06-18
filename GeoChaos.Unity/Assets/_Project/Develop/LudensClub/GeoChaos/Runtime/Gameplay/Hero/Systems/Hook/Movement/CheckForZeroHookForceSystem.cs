using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Hook
{
  public class CheckForZeroHookForceSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly SpeedForceLoop _hookForces;
    private readonly EcsEntities _pullings;

    public CheckForZeroHookForceSystem(GameWorldWrapper gameWorldWrapper, ISpeedForceLoopService forceLoopSvc)
    {
      _game = gameWorldWrapper.World;
      _hookForces = forceLoopSvc.CreateLoop();

      _pullings = _game
        .Filter<HookPulling>()
        .Exc<InterruptHookCommand>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity pulling in _pullings)
      {
        foreach (EcsEntity force in _hookForces
          .GetLoop(SpeedForceType.Hook, pulling.Pack()))
        {
          ref MovementVector vector = ref force.Get<MovementVector>();
          if (vector.Speed == Vector2.zero && force.Get<Impact>().Vector != Vector2.zero)
            pulling.Add<InterruptHookCommand>();
        }
      }
    }
  }
}