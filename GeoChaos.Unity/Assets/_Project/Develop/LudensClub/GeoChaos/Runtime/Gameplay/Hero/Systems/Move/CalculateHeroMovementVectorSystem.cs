using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Move;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class CalculateHeroMovementVectorSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly HeroConfig _config;
    private readonly EcsEntities _movings;
    private readonly EcsEntities _commands;

    public CalculateHeroMovementVectorSystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _commands = _game
        .Filter<Movable>()
        .Inc<MoveCommand>()
        .Inc<MovementVector>()
        .Collect();

      _movings = _game
        .Filter<Movable>()
        .Inc<Moving>()
        .Inc<MovementVector>()
        .Exc<MoveCommand>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity command in _commands)
      {
        float direction = command.Get<MoveCommand>().Direction;
        float speed = command.Get<HorizontalSpeed>().Value;
        float delta = CalculateSpeedDelta(speed);

        command.Replace((ref MovementVector vector) =>
        {
          vector.Speed.x += delta;
          vector.Direction.x = direction;
          vector.Speed.x = Mathf.Clamp(vector.Speed.x, 0, speed);
        });

        command.Is<Moving>(true);
      }

      foreach (EcsEntity moving in _movings)
      {
        float speed = moving.Get<HorizontalSpeed>().Value;
        float delta = CalculateSpeedDelta(speed);

        ref MovementVector vector = ref moving.Get<MovementVector>();
        vector.Speed.x -= delta;
        vector.Speed.x = Mathf.Clamp(vector.Speed.x, 0, speed);

        moving.Is<Moving>(vector.Speed.x != 0);
      }
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