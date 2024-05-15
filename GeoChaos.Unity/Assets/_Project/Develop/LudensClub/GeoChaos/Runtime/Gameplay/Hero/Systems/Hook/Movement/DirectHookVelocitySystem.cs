using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Hook
{
  public class DirectHookVelocitySystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _pullings;
    private readonly SpeedForceLoop _forceLoop;

    public DirectHookVelocitySystem(GameWorldWrapper gameWorldWrapper, ISpeedForceLoopService forceLoopSvc)
    {
      _game = gameWorldWrapper.World;

      _forceLoop = forceLoopSvc.CreateLoop();

      _pullings = _game
        .Filter<HookPulling>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity pulling in _pullings)
      {
        ref HookPulling hookPulling = ref pulling.Get<HookPulling>();
        foreach (EcsEntity force in _forceLoop
          .GetLoop(SpeedForceType.Hook, pulling.Pack()))
        {
          ref MovementVector forceVector = ref force.Get<MovementVector>();
          float magnitude = forceVector.Speed.magnitude;
          Vector2 targetVector = hookPulling.Target - pulling.Get<ViewRef>().View.transform.position;
          Vector2 velocity = targetVector.normalized * magnitude;
          (Vector3 length, Vector3 direction) = MathUtils.DecomposeVector(velocity);
          forceVector.Speed = length;
          forceVector.Direction = direction;
          hookPulling.Velocity = velocity;
        }
      }
    }
  }
}