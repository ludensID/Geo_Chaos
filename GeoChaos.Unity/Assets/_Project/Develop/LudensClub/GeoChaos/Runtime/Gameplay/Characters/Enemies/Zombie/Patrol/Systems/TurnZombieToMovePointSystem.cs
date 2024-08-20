using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Patrol;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Move;
using LudensClub.GeoChaos.Runtime.Gameplay.View;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Patrol
{
  public class TurnZombieToMovePointSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _patrollingZombies;

    public TurnZombieToMovePointSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _patrollingZombies = _game
        .Filter<ZombieTag>()
        .Inc<OnPatrolStarted>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity zombie in _patrollingZombies)
      {
        float currentPoint = zombie.Get<ViewRef>().View.transform.position.x;
        float nextPoint = zombie.Get<MovePoint>().Point;
        zombie.Change((ref BodyDirection bodyDirection) =>
          bodyDirection.Direction = Mathf.Sign(nextPoint - currentPoint));
      }
    }
  }
}