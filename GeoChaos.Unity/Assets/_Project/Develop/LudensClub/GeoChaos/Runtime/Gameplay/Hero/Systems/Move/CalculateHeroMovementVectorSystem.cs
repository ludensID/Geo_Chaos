using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Move;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;
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
        float speed = command.Get<HorizontalSpeed>().Value;
        float acceleration = CalculateAcceleration(speed);

        _forceFactory.Create(new SpeedForceData(SpeedForceType.Move, command.Pack(), Vector2.right)
        {
          Direction = new Vector2(direction, 0),
          Accelerated = true,
          MaxSpeed = speed,
          Acceleration = new Vector2(acceleration, 0)
        });
        
        command.Add<Moving>();
      }
      
      foreach (EcsEntity command in _commands)
      {
        float direction = command.Get<MoveCommand>().Direction;
        float speed = command.Get<HorizontalSpeed>().Value;
        float acceleration = CalculateAcceleration(speed);
        
        foreach (EcsEntity force in _forces
          .GetLoop(SpeedForceType.Move, command.Pack()))
        {
          force
            .Replace((ref MovementVector vector) => vector.Direction.x = direction)
            .Replace((ref Acceleration a) => a.Value.x = acceleration)
            .Replace((ref MaxSpeed maxSpeed) => maxSpeed.Speed = speed);
        }
      }

      foreach (EcsEntity moving in _movings)
      {
        float speed = moving.Get<HorizontalSpeed>().Value;
        float acceleration = CalculateAcceleration(speed);
        
        foreach (EcsEntity force in _forces
          .GetLoop(SpeedForceType.Move, moving.Pack()))
        {
          force
            .Replace((ref MaxSpeed maxSpeed) => maxSpeed.Speed = speed)
            .Replace((ref Acceleration a) => a.Value.x = -acceleration);
          
          if (force.Get<MovementVector>().Speed.x <= 0)
          {
            force.Replace((ref Impact impact) => impact.Vector.x = 0);
            moving.Del<Moving>();
          }
        }
      }
    }
    
    private float CalculateAcceleration(float speed)
    {
      return _config.AccelerationTime == 0
        ? speed / Time.fixedDeltaTime * 100
        : speed / _config.AccelerationTime;
    }
  }
}