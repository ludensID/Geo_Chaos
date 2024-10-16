﻿using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Move;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Move
{
  public class CalculateHeroMovementVectorSystem : IEcsRunSystem
  {
    private readonly ISpeedForceFactory _forceFactory;
    private readonly EcsWorld _game;
    private readonly HeroConfig _config;
    private readonly EcsEntities _stopMovingCommands;
    private readonly EcsEntities _continueMovingCommands;
    private readonly EcsEntities _startMovingCommands;
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

      _startMovingCommands = _game
        .Filter<HeroTag>()
        .Inc<Movable>()
        .Inc<MovementVector>()
        .Inc<MoveHeroCommand>()
        .Exc<Moving>()
        .Collect();

      _continueMovingCommands = _game
        .Filter<HeroTag>()
        .Inc<Movable>()
        .Inc<MovementVector>()
        .Inc<MoveHeroCommand>()
        .Inc<Moving>()
        .Collect();

      _stopMovingCommands = _game
        .Filter<HeroTag>()
        .Inc<Movable>()
        .Inc<MovementVector>()
        .Inc<Moving>()
        .Exc<MoveHeroCommand>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity command in _startMovingCommands)
      {
        float direction = command.Get<MoveHeroCommand>().Direction;
        (float normalized, float speed, float acceleration) = GetSpeedForceValues(command, direction);

        command.Change((ref HeroMoveDirection moveDirection) => moveDirection.Direction.x = direction);

        CreateMoveSpeedForce(command.PackedEntity, normalized, speed, acceleration);

        command.Add<Moving>();
      }

      foreach (EcsEntity command in _continueMovingCommands)
      {
        float direction = command.Get<MoveHeroCommand>().Direction;
        (float normalized, float speed, float acceleration) = GetSpeedForceValues(command, direction);

        command.Change((ref HeroMoveDirection moveDirection) => moveDirection.Direction.x = direction);

        EcsEntities forces = _forces.GetLoop(SpeedForceType.Move, command.PackedEntity);
        ChangeMoveSpeedForce(forces, normalized, acceleration, speed);

        if (!forces.Any())
          command.Del<Moving>();
      }

      foreach (EcsEntity moving in _stopMovingCommands)
      {
        EcsEntities forces = _forces.GetLoop(SpeedForceType.Move, moving.PackedEntity);
        if (!forces.Any())
        {
          moving.Del<Moving>();
          continue;
        }

        float direction = moving.Get<HeroMoveDirection>().Direction.x;
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