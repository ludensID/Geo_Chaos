using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class JumpHeroViewSystem : IEcsRunSystem
  {
    private readonly EcsWorld _world;
    private readonly EcsFilter _heroes;
    private readonly HeroConfig _config;

    public JumpHeroViewSystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _world = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _heroes = _world.Filter<Hero>()
        .Inc<JumpAvailable>()
        .Inc<JumpCommand>()
        .Inc<RigidbodyRef>()
        .End();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (int hero in _heroes)
      {
        ref RigidbodyRef rigidbodyRef = ref _world.Get<RigidbodyRef>(hero);
        Vector2 velocity = new Vector2(rigidbodyRef.Rigidbody.velocity.x, 0);
        velocity.y = 0;
        rigidbodyRef.Rigidbody.velocity = velocity;
        rigidbodyRef.Rigidbody.AddForce(Vector2.up * _config.JumpForce, ForceMode2D.Impulse);

        ref JumpAvailable jump = ref _world.Get<JumpAvailable>(hero);
        jump.IsJumping = true;

        _world.Del<JumpCommand>(hero);
      }
    }
  }
}