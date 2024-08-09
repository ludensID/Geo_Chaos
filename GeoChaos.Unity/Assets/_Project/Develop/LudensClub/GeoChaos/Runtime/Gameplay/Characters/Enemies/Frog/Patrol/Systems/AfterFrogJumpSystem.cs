using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Patrol;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.JumpWait;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Jump;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Patrol
{
  public class AfterFrogJumpSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _patrollingFrogs;
    private readonly FrogConfig _config;

    public AfterFrogJumpSystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<FrogConfig>();

      _patrollingFrogs = _game
        .Filter<FrogTag>()
        .Inc<Patrolling>()
        .Inc<OnJumpFinished>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity frog in _patrollingFrogs)
      {
        float currentPoint = frog.Get<ViewRef>().View.transform.position.x;
        float nextPoint = frog.Get<PatrolPoint>().Point;
        if (Mathf.Abs(nextPoint - currentPoint) <= _config.SmallJumpLength)
          frog.Add<FinishPatrolCommand>();
        else
          frog.Add<WaitJumpCommand>();
      }
    }
  }
}