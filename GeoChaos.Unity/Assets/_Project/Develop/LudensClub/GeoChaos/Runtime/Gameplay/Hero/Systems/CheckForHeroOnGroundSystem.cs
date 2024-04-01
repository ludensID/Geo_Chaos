using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class CheckForHeroOnGroundSystem : IEcsRunSystem
  {
    private readonly EcsWorld _world;
    private readonly PhysicsConfig _physics;
    private readonly EcsFilter _heroes;

    public CheckForHeroOnGroundSystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _world = gameWorldWrapper.World;
      _physics = configProvider.Get<PhysicsConfig>();

      _heroes = _world.Filter<Hero>()
        .Inc<Ground>()
        .Inc<GroundCheckRef>()
        .End();
    }

    public void Run(EcsSystems systems)
    {
      foreach (int hero in _heroes)
      {
        ref GroundCheckRef groundCheckRef = ref _world.Get<GroundCheckRef>(hero);
        ref Ground ground = ref _world.Get<Ground>(hero);
        ground.IsOnGround = IsGroundCasted(groundCheckRef.Bottom.position);
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