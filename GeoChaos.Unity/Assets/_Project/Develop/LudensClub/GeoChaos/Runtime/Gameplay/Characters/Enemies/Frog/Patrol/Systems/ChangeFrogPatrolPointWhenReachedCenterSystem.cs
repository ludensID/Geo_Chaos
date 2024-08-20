using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Patrol;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.JumpCycle;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Jump;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Patrol
{
  public class ChangeFrogPatrolPointWhenReachedCenterSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _patrollingFrogs;

    public ChangeFrogPatrolPointWhenReachedCenterSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

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
        Transform transform = frog.Get<ViewRef>().View.transform;
        float currentPoint = transform.position.x;
        Vector2 bounds = frog.Get<PatrolBounds>().HorizontalBounds;
        float center = (bounds.x + bounds.y) / 2;
        ref JumpPoint jumpPoint = ref frog.Get<JumpPoint>();

        if (jumpPoint.Point.ApproximatelyEqual(center)
          && (currentPoint - center) * transform.right.x > 0)
        {
          jumpPoint.Point = bounds[Random.Range(0, 2)];
        }
      }
    }
  }
}