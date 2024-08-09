using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Patrol;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Jump;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.JumpWait;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Jump;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Patrol
{
  public class PrepareFrogJumpDuringPatrollingSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _startPatrollingFrogs;
    private readonly FrogConfig _config;
    private readonly EcsEntities _patrollingFrogs;

    public PrepareFrogJumpDuringPatrollingSystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<FrogConfig>();

      _startPatrollingFrogs = _game
        .Filter<FrogTag>()
        .Inc<OnPatrolStarted>()
        .Collect();

      _patrollingFrogs = _game
        .Filter<FrogTag>()
        .Inc<OnJumpWaitFinished>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity frog in _startPatrollingFrogs)
      {
        PrepareJump(frog);
      }

      foreach (EcsEntity frog in _patrollingFrogs)
      {
        PrepareJump(frog);
      }
    }

    private void PrepareJump(EcsEntity frog)
    {
      float currentPoint = frog.Get<ViewRef>().View.transform.position.x;
      float nextPoint = frog.Get<PatrolPoint>().Point;
      
      frog
        .Add<JumpCommand>()
        .Add((ref FrogJumpContext ctx) =>
        {
          ctx.Direction = Mathf.Sign(nextPoint - currentPoint);
          ctx.Length = _config.SmallJumpLength;
          ctx.Height = _config.SmallJumpHeight;
        });
    }
  }
}