using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.AI;
using LudensClub.GeoChaos.Runtime.Gameplay.AI;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemies.Lama
{
  public class PatrolLamaSystem : IEcsRunSystem
  {
    private readonly ISpeedForceFactory _forceFactory;
    private readonly ITimerFactory _timers;
    private readonly EcsWorld _game;
    private readonly EcsEntities _commands;

    public PatrolLamaSystem(GameWorldWrapper gameWorldWrapper, ISpeedForceFactory forceFactory, ITimerFactory timers)
    {
      _forceFactory = forceFactory;
      _timers = timers;
      _game = gameWorldWrapper.World;

      _commands = _game
        .Filter<LamaTag>()
        .Inc<PatrolCommand>()
        .Exc<Patrolling>()
        .Exc<LookingTimer>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity command in _commands)
      {
        var ctx = command.Get<BrainContext>().Cast<LamaContext>();
        Vector2 bounds = command.Get<PatrolBounds>().Bounds;
        Vector3 position = command.Get<ViewRef>().View.transform.position;

        float direction = CalculateDirection(position.x, ctx.PatrolStep, bounds);

        if (direction != 0)
        {
          _forceFactory.Create(new SpeedForceData(SpeedForceType.Move, command.Pack(), Vector2.right)
          {
            Speed = Vector2.right * ctx.MovementSpeed,
            Direction = Vector2.right * direction
          });

          command
            .Add<Patrolling>()
            .Add((ref PatrollingTimer timer) => timer.TimeLeft = _timers.Create(ctx.PatrolStep / ctx.MovementSpeed));
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