using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Hook
{
  public class CheckForHeroReachRingSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _pullings;
    private readonly SpeedForceLoop _forceLoop;

    public CheckForHeroReachRingSystem(GameWorldWrapper gameWorldWrapper, ISpeedForceLoopService forceLoopSvc)
    {
      _game = gameWorldWrapper.World;

      _forceLoop = forceLoopSvc.CreateLoop();

      _pullings = _game
        .Filter<HookPulling>()
        .Inc<ViewRef>()
        .Inc<MovementVector>()
        .Exc<StopHookPullingCommand>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity pulling in _pullings)
      {
        Transform heroTransform = pulling.Get<ViewRef>().View.transform;
        ref HookPulling hookPulling = ref pulling.Get<HookPulling>();
        if (IsHeroReachedRing(hookPulling.Target, heroTransform.position, hookPulling.Velocity))
        {
          foreach (EcsEntity force in _forceLoop
            .GetLoop(SpeedForceType.Hook, pulling.Pack()))
          {
            force.Add<Instant>();
          }

          pulling.Add<StopHookPullingCommand>()
            .Replace((ref GravityScale gravity) =>
            {
              gravity.Enabled = true;
              gravity.Override = true;
            });
        }
      }
    }

    private static bool IsHeroReachedRing(Vector2 ring, Vector2 hero, Vector2 velocity)
    {
      Vector2 distance = ring - hero;
      Vector2 delta = distance * velocity;
      return delta is { x: <= 0, y: <= 0 } || distance.sqrMagnitude <= 0.1;
    }
  }
}