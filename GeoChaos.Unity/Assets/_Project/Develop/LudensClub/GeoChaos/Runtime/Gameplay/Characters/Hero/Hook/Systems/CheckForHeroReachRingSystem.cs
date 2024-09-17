using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Gravity;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Hook
{
  public class CheckForHeroReachRingSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _pullings;
    private readonly SpeedForceLoop _forces;

    public CheckForHeroReachRingSystem(GameWorldWrapper gameWorldWrapper, ISpeedForceLoopService forceLoopSvc)
    {
      _game = gameWorldWrapper.World;

      _forces = forceLoopSvc.CreateLoop();
      
      _pullings = _game
        .Filter<HeroTag>()
        .Inc<HookPulling>()
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
          foreach (EcsEntity force in _forces
            .GetLoop(SpeedForceType.Hook, pulling.PackedEntity))
          {
            force
              .Change((ref Impact impact) => impact.Vector.y = 0)
              .Add<Valuable>()
              .Add<Residual>();
          }

          pulling
            .Replace((ref ActionState actionState) => actionState.States.Add(StateType.Finish))
            .Add<StopHookPullingCommand>()
            .Change((ref GravityScale gravity) => gravity.Enabled.Value = true);
        }
      }
    }

    private static bool IsHeroReachedRing(Vector2 ring, Vector2 hero, Vector2 velocity)
    {
      Vector2 distance = ring - hero;
      Vector2 delta = distance * velocity;
      return delta.x <= 0 || delta.y <= 0 || distance.sqrMagnitude <= 0.1;
    }
  }
}