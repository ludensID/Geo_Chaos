using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;
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
          if (force.Get<MovementVector>().Speed == Vector2.zero)
            pulling.Add<InterruptHookCommand>();
        }
      }
    }
  }
}