using System.Linq;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Move;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Move;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class CalculateHeroMovementVectorSystem : IEcsRunSystem
  {
    private readonly ISpeedForceFactory _forceFactory;
    private readonly EcsWorld _game;
    private readonly HeroConfig _config;
    private readonly EcsEntities _movings;
    private readonly EcsEntities _commands;
    private readonly EcsEntities _startCommands;
    private readonly SpeedForceLoop _forces;

    public CalculateHeroMovementVectorSystem(GameWorldWrapper gameWorldWrapper,
      ISpeedForceLoopService forceLoopSvc,
      ISpeedForceFactory forceFactory,
      IConfigProvider configProvider)
    {
      _forceFactory = forceFactory;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _forces = forceLoopSvc.CreateLoop();

      _startCommands = _game
        .Filter<Movable>()
        .Inc<MovementVector>()
        .Inc<MoveCommand>()
        .Exc<Moving>()
        .Collect();

      _commands = _game
        .Filter<Movable>()
        .Inc<MovementVector>()
        .Inc<MoveCommand>()
        .Inc<Moving>()
        .Collect();

      _movings = _game
        .Filter<Movable>()
        .Inc<MovementVector>()
        .Inc<Moving>()
        .Exc<MoveCommand>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity command in _startCommands)
      {
        float direction = command.Get<MoveCommand>().Direction;
        (float normalized, float speed, float acceleration) = GetSpeedForceValues(command, direction);

        command.Change((ref MoveDirection moveDirection) => moveDirection.Direction.x = direction);

        CreateMoveSpeedForce(command.Pack(), normalized, speed, acceleration);

        command.Add<Moving>();
      }

      foreach (EcsEntity command in _commands)
      {
        float direction = command.Get<MoveCommand>().Direction;
        (float normalized, float speed, float acceleration) = GetSpeedForceValues(command, direction);

        command.Change((ref MoveDirection moveDirection) => moveDirection.Direction.x = direction);

        EcsEntities forces = _forces.GetLoop(SpeedForceType.Move, command.Pack());
        ChangeMoveSpeedForce(forces, normalized, acceleration, speed);

        if (!forces.Any())
          command.Del<Moving>();
      }

      foreach (EcsEntity moving in _movings)
      {
        EcsEntities forces = _forces.GetLoop(SpeedForceType.Move, moving.Pack());
        if (!forces.Any())
        {
          moving.Del<Moving>();
          continue;
        }

        float direction = moving.Get<MoveDirection>().Direction.x;
        float speed = moving.Get<HorizontalSpeed>().Speed * Mathf.Abs(direction);
        float acceleration = CalculateAcceleration(speed);

        DecreaseMoveSpeedForce(forces, speed, acceleration);
      }
    }

    private void DecreaseMoveSpeedForce(EcsEntities forces, float speed, float acceleration)
    {
      foreach (EcsEntity force in forces)
      {
        force
          .Change((ref MaxSpeed maxSpeed) => maxSpeed.Speed = speed)
          .Change((ref Acceleration a) => a.Value.x = -acceleration);

        if (force.Get<MovementVector>().Speed.x <= 0)
          force.Change((ref Impact impact) => impact.Vector.x = 0);
      }
    }

    private void ChangeMoveSpeedForce(EcsEntities forces, float normalized, float acceleration, float speed)
    {
      foreach (EcsEntity force in forces)
      {
        force
          .Change((ref MovementVector vector) => vector.Direction.x = normalized)
          .Change((ref Acceleration a) => a.Value.x = acceleration)
          .Change((ref MaxSpeed maxSpeed) => maxSpeed.Speed = speed);
      }
    }

    private (float normalized, float speed, float acceleration) GetSpeedForceValues(EcsEntity entity,
      float direction)
    {
      float normalized = direction != 0 ? Mathf.Sign(direction) : 0;
      float speed = entity.Get<HorizontalSpeed>().Speed * Mathf.Abs(direction);
      float acceleration = CalculateAcceleration(speed);
      return (normalized, speed, acceleration);
    }

    private void CreateMoveSpeedForce(EcsPackedEntity owner, float normalizedDirection, float speed, float acceleration)
    {
      _forceFactory.Create(new SpeedForceData(SpeedForceType.Move, owner, Vector2.right)
      {
        Direction = new Vector2(normalizedDirection, 0),
        Accelerated = true,
        MaxSpeed = speed,
        Acceleration = new Vector2(acceleration, 0)
      });
    }

    private float CalculateAcceleration(float speed)
    {
      return _config.AccelerationTime == 0
        ? speed / Time.fixedDeltaTime * 100
        : speed / _config.AccelerationTime;
    }
  }
}