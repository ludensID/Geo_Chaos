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
        Vector3 startPosition = command.Get<StartTransform>().Position;
        Vector3 position = command.Get<ViewRef>().View.transform.position;

        float direction = CalculateDirection(startPosition.x, ctx.PatrolAreaLength / 2, position.x, ctx.PatrolStep);

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

    private static float CalculateDirection(float origin, float radius, float pos, float step)
    {
      float distance = origin - pos;

      int left = -1;
      int right = 1;

      if (OutOfBounds(right))
        right = left;

      if (OutOfBounds(left))
        left = right;

      return Mathf.Sign(Random.Range(left, right));

      bool OutOfBounds(float direction)
      {
        return radius < Mathf.Abs(distance - step * direction);
      }
    }
  }
}