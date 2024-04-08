using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class CheckForHeroOnGroundSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly PhysicsConfig _physics;
    private readonly EcsFilter _grounds;
    private readonly EcsFilter _onGrounds;

    public CheckForHeroOnGroundSystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _game = gameWorldWrapper.World;
      _physics = configProvider.Get<PhysicsConfig>();

      _grounds = _game
        .Filter<Ground>()
        .Inc<GroundCheckRef>()
        .Exc<IsOnGround>()
        .End();

      _onGrounds = _game
        .Filter<IsOnGround>()
        .Inc<GroundCheckRef>()
        .End();
    }

    public void Run(EcsSystems systems)
    {
      foreach (int ground in _grounds)
      {
        ref GroundCheckRef groundCheckRef = ref _game.Get<GroundCheckRef>(ground);
        if (IsGroundCasted(groundCheckRef.Bottom.position))
        {
          _game.Add<IsOnGround>(ground);
          _game.Add<OnGround>(ground);
        }
      }

      foreach (int onGround in _onGrounds)
      {
        ref GroundCheckRef groundCheckRef = ref _game.Get<GroundCheckRef>(onGround);
        if (!IsGroundCasted(groundCheckRef.Bottom.position))
        {
          _game.Del<IsOnGround>(onGround);
        }
      }
    }

    private bool IsGroundCasted(Vector3 position)
    {
      var origin = new Vector2(position.x, position.y);
      Vector2 direction = Vector2.zero;
      RaycastHit2D raycastHit = Physics2D.CircleCast(origin, _physics.AcceptableGroundDistance, direction,
        Mathf.Infinity, _physics.GroundLayer);
      return raycastHit.collider != null;
    }
  }
}