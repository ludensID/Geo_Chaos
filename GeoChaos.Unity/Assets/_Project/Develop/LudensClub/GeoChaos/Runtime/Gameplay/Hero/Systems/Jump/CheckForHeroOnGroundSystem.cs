using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Jump;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class CheckForHeroOnGroundSystem : IEcsRunSystem
  {
    private readonly ITimerFactory _timers;
    private readonly EcsWorld _game;
    private readonly PhysicsConfig _physics;
    private readonly EcsEntities _grounds;
    private readonly EcsEntities _onGrounds;

    public CheckForHeroOnGroundSystem(GameWorldWrapper gameWorldWrapper,
      IConfigProvider configProvider,
      ITimerFactory timers)
    {
      _timers = timers;
      _game = gameWorldWrapper.World;
      _physics = configProvider.Get<PhysicsConfig>();

      _grounds = _game
        .Filter<Ground>()
        .Inc<GroundCheckRef>()
        .Inc<GroundCheckTimer>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity ground in _grounds
        .Where<GroundCheckTimer>(x => x.TimeLeft <= 0))
      {
        ref GroundCheckRef checkRef = ref ground.Get<GroundCheckRef>();
        bool onGround = ground.Has<OnGround>();
        bool isGroundCasted = IsGroundCasted(checkRef.Bottom.position, true);
        switch (onGround, isGroundCasted)
        {
          case (true, false):
            ground
              .Del<OnGround>()
              .Add<OnLeftGround>();
            break;
          case (false, true):
            ground
              .Add<OnGround>()
              .Add<OnLanded>();
            break;
        }

        if (onGround != isGroundCasted)
        {
          ground.Replace((ref GroundCheckTimer timer) => timer.TimeLeft = _timers.Create(_physics.GroundCheckTime));
        }
      }
    }

    private bool IsGroundCasted(Vector3 position, bool waiting)
    {
      RaycastHit2D raycastHit = Physics2D.CircleCast(position, _physics.AcceptableGroundDistance, Vector2.zero,
        Mathf.Infinity, _physics.GroundMask);

      return raycastHit.collider != null;
    }
  }
}