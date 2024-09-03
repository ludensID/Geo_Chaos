using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Gravity
{
  public class CheckForEntityOnGroundSystem : IEcsRunSystem
  {
    private readonly ITimerFactory _timers;
    private readonly EcsWorld _game;
    private readonly PhysicsConfig _physics;
    private readonly EcsEntities _grounds;
    private readonly EcsEntities _onGrounds;
    private ContactFilter2D _filter;
    private readonly RaycastHit2D[] _hits;

    public CheckForEntityOnGroundSystem(GameWorldWrapper gameWorldWrapper,
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

      _hits = new RaycastHit2D[5];
      _filter = new ContactFilter2D
      {
        useLayerMask = true,
        layerMask = _physics.GroundMask,
        useTriggers = false
      };
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity ground in _grounds
        .Check<GroundCheckTimer>(x => x.TimeLeft <= 0))
      {
        ref GroundCheckRef checkRef = ref ground.Get<GroundCheckRef>();
        bool onGround = ground.Has<OnGround>();
        bool isGroundCasted = IsGroundCasted(checkRef.Bottom.position);
        switch (onGround, isGroundCasted)
        {
          case (true, false):
            ground
              .Del<OnGround>()
              .Add<OnLifted>();
            break;
          case (false, true):
            ground
              .Add<OnGround>()
              .Has<OnLanded>(true);
            break;
        }

        if (onGround != isGroundCasted)
        {
          ground.Change((ref GroundCheckTimer timer) => timer.TimeLeft = _timers.Create(_physics.GroundCheckTime));
        }
      }
    }

    private bool IsGroundCasted(Vector3 position)
    {
      return 0 < Physics2D.CircleCast(position, _physics.AcceptableGroundDistance, Vector2.zero,_filter, _hits);
    }
  }
}