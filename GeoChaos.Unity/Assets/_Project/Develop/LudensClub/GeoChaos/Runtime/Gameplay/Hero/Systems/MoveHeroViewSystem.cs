using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class MoveHeroViewSystem : IEcsRunSystem
  {
    private readonly EcsWorld _world;
    private readonly EcsFilter _heroes;

    public MoveHeroViewSystem(GameWorldWrapper gameWorldWrapper)
    {
      _world = gameWorldWrapper.World;
      _heroes = _world.Filter<Hero>()
        .Inc<Movable>()
        .Inc<RigidbodyRef>()
        .End();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (int hero in _heroes)
      {
        ref HeroVelocity velocity = ref _world.Get<HeroVelocity>(hero);
        ref RigidbodyRef rigidbodyRef = ref _world.Get<RigidbodyRef>(hero);
        
        Vector3 velocityVector = rigidbodyRef.Rigidbody.velocity;
        if (velocity.OverrideVelocityX)
          velocityVector.x = velocity.Velocity.x;
        if (velocity.OverrideVelocityY)
          velocityVector.y = velocity.Velocity.y;
        rigidbodyRef.Rigidbody.velocity = velocityVector;
        velocity.Velocity = velocityVector;

        velocity.OverrideVelocityX = false;
        velocity.OverrideVelocityY = false;
      }
    }
  }
}