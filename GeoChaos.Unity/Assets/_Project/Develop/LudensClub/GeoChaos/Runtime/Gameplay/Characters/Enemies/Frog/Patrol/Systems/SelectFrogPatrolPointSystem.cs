using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Patrol;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Jump;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.JumpCycle;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Patrol
{
  public class SelectFrogPatrolPointSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _patrollingFrogs;
    private readonly FrogConfig _config;

    public SelectFrogPatrolPointSystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<FrogConfig>();

      _patrollingFrogs = _game
        .Filter<FrogTag>()
        .Inc<PatrolCommand>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity frog in _patrollingFrogs)
      {
        float currentPoint = frog.Get<ViewRef>().View.transform.position.x;
        Vector2 bounds = frog.Get<PatrolBounds>().Bounds;
        float center = (bounds.x + bounds.y) / 2;

        float nextPoint = Mathf.Abs(currentPoint - center) <= _config.SmallJumpLength 
            ? bounds[Random.Range(0, 2)] 
            : center;

        frog
          .Del<PatrolCommand>()
          .Add<Patrolling>()
          .Add<StartJumpCycleCommand>()
          .Replace((ref JumpPoint point) => point.Point = nextPoint)
          .Replace((ref FrogJumpContext ctx) =>
          {
            ctx.Length = _config.SmallJumpLength;
            ctx.Height = _config.SmallJumpHeight;
          });
      }
    }
  }
}