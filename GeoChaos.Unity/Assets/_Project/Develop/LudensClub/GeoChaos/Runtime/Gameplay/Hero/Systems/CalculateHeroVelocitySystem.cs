using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class CalculateHeroVelocitySystem : IEcsRunSystem
  {
    private readonly EcsWorld _world;
    private readonly EcsFilter _heroes;
    private readonly HeroConfig _config;

    public CalculateHeroVelocitySystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _world = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();
      
      _heroes = _world.Filter<Hero>()
        .Inc<Movable>()
        .Inc<MovementQueue>()
        .Inc<HeroMovementVector>()
        .Inc<HeroVelocity>()
        .End();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (int hero in _heroes)
      {
        ref MovementQueue movement = ref _world.Get<MovementQueue>(hero);
        if (!movement.NextMovements.TryPeek(out DelayedMovement delayedMovement) || delayedMovement.WaitingTime > 0)
          continue;

        ref HeroMovementVector vector = ref _world.Get<HeroMovementVector>(hero);
        float delta = _config.MovementSpeed / _config.AccelerationTime * Time.deltaTime;
        if (delayedMovement.Direction == 0)
        {
          vector.Speed -= delta;
        }
        else
        {
          vector.Speed += delta;
          vector.Direction = delayedMovement.Direction;
        }

        vector.Speed = Mathf.Clamp(vector.Speed, 0, _config.MovementSpeed);

        ref HeroVelocity velocity = ref _world.Get<HeroVelocity>(hero);
        velocity.Velocity.x = vector.Speed * vector.Direction;
        velocity.OverrideVelocityX = true;
        
        movement.NextMovements.Dequeue();
      }
    }
  }
}