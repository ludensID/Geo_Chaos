using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.AI;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Lama.Patrol
{
  public class PatrolLamaSystem : IEcsRunSystem
  {
    private readonly ISpeedForceFactory _forceFactory;
    private readonly ITimerFactory _timers;
    private readonly EcsWorld _game;
    private readonly EcsEntities _commands;
    private readonly LamaConfig _config;

    public PatrolLamaSystem(GameWorldWrapper gameWorldWrapper,
      ISpeedForceFactory forceFactory,
      ITimerFactory timers,
      IConfigProvider configProvider)
    {
      _forceFactory = forceFactory;
      _timers = timers;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<LamaConfig>();

      _commands = _game
        .Filter<LamaTag>()
        .Inc<PatrolCommand>()
        .Exc<Patrolling>()
        .Exc<WaitingTimer>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity command in _commands)
      {
        Vector2 bounds = command.Get<PatrolBounds>().Bounds;
        Vector3 position = command.Get<ViewRef>().View.transform.position;

        float step = Random.Range(_config.PatrolStep.x, _config.PatrolStep.y);
        float direction = CalculateDirection(position.x, step, bounds);

        if (direction != 0)
        {
          _forceFactory.Create(new SpeedForceData(SpeedForceType.Move, command.Pack(), Vector2.right)
          {
            Speed = Vector2.right * _config.MovementSpeed,
            Direction = Vector2.right * direction,
            Unique = true
          });

          command
            .Add<Patrolling>()
            .Add((ref PatrollingTimer timer) => timer.TimeLeft = _timers.Create(step / _config.MovementSpeed));
        }
      }
    }

    private static float CalculateDirection(float position, float step, Vector2 bounds)
    {
      int left = -1;
      int right = 1;

      if (OutOfBounds(right))
        right = 0;

      if (OutOfBounds(left))
        left = 0;

      // [-1, 1) = [-1, +0] (random)
      // [-1, 0) = -1 (left)
      // [0, 1) = +0 (right)
      // [0, 0) = 0 (noting)
      return right == 0 && left == 0 ? 0 : Mathf.Sign(Random.Range(left, right));

      bool OutOfBounds(float direction)
      {
        float next = position + step * direction;
        return next < bounds.x || next > bounds.y;
      }
    }
  }
}