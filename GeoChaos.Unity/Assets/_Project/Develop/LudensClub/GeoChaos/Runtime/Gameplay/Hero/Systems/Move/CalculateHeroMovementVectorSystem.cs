using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class CalculateHeroMovementVectorSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsFilter _heroes;
    private readonly HeroConfig _config;

    public CalculateHeroMovementVectorSystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _heroes = _game.Filter<HeroTag>()
        .Inc<Movable>()
        .Inc<MoveCommand>()
        .Inc<MovementVector>()
        .End();
    }

    public void Run(EcsSystems systems)
    {
      foreach (int hero in _heroes)
      {
        ref MoveCommand command = ref _game.Get<MoveCommand>(hero);
        ref HorizontalSpeed speed = ref _game.Get<HorizontalSpeed>(hero);
        ref MovementVector vector = ref _game.Get<MovementVector>(hero);
        CalculateVector(ref vector, command.Direction, speed.Value);
      }
    }

    private void CalculateVector(ref MovementVector vector, float direction, float speed)
    {
      float delta = CalculateSpeedDelta(speed);
      if (direction == 0)
      {
        vector.Speed.x -= delta;
      }
      else
      {
        vector.Speed.x += delta;
        vector.Direction.x = direction;
      }

      vector.Speed.x = Mathf.Clamp(vector.Speed.x, 0, speed);
    }

    private float CalculateSpeedDelta(float speed)
    {
      float delta = _config.AccelerationTime == 0
        ? speed
        : speed / _config.AccelerationTime * Time.deltaTime;
      return delta;
    }
  }
}