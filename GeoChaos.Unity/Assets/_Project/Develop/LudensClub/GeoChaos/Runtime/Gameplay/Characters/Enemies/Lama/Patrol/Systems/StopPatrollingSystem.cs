using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Patrol;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Lama.Patrol
{
  public class StopPatrollingSystem : IEcsRunSystem
  {
    private readonly ISpeedForceFactory _forceFactory;
    private readonly EcsWorld _game;
    private readonly EcsEntities _lamasCommands;

    public StopPatrollingSystem(GameWorldWrapper gameWorldWrapper, ISpeedForceFactory forceFactory)
    {
      _forceFactory = forceFactory;
      _game = gameWorldWrapper.World;

      _lamasCommands = _game
        .Filter<LamaTag>()
        .Inc<StopPatrolCommand>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity command in _lamasCommands)
      {
        command.Del<StopPatrolCommand>();

        if (command.Has<Patrolling>())
        {
          command
            .Del<Patrolling>()
            .Del<PatrollingTimer>();

          _forceFactory.Create(new SpeedForceData(SpeedForceType.Move, command.PackedEntity, Vector2.right)
          {
            Instant = true
          });
        }
      }
    }
  }
}