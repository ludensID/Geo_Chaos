using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class CalculateHeroMovementVectorSystem : IEcsRunSystem
  {
    private readonly EcsWorld _world;
    private readonly EcsFilter _heroes;
    private readonly HeroConfig _config;

    public CalculateHeroMovementVectorSystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _world = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _heroes = _world.Filter<Hero>()
        .Inc<Movable>()
        .Inc<MoveCommand>()
        .Inc<HeroMovementVector>()
        .End();
    }

    public void Run(EcsSystems systems)
    {
      foreach (int hero in _heroes)
      {
        ref MoveCommand command = ref _world.Get<MoveCommand>(hero);
        
        ref HeroMovementVector vector = ref _world.Get<HeroMovementVector>(hero);
        CalculateVector(ref vector, command.Direction);
      }
    }

    private void CalculateVector(ref HeroMovementVector vector, float direction)
    {
      float delta = CalculateSpeedDelta();
      if (direction == 0)
      {
        vector.Speed.x -= delta;
      }
      else
      {
        vector.Speed.x += delta;
        vector.Direction.x = direction;
      }

      vector.Speed.x = Mathf.Clamp(vector.Speed.x, 0, _config.MovementSpeed);
    }

    private float CalculateSpeedDelta()
    {
      float delta = _config.AccelerationTime == 0
        ? _config.MovementSpeed
        : _config.MovementSpeed / _config.AccelerationTime * Time.deltaTime;
      return delta;
    }
  }
}