using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Patrol;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Move;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Patrol
{
  public class ZombiePatrollingSystem : IEcsRunSystem
  {
    private readonly ISpeedForceFactory _forceFactory;
    private readonly EcsWorld _game;
    private readonly EcsEntities _patrollingZombies;
    private readonly ZombieConfig _config;

    public ZombiePatrollingSystem(GameWorldWrapper gameWorldWrapper,
      IConfigProvider configProvider,
      ISpeedForceFactory forceFactory)
    {
      _forceFactory = forceFactory;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<ZombieConfig>();

      _patrollingZombies = _game
        .Filter<ZombieTag>()
        .Inc<PatrolCommand>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity zombie in _patrollingZombies)
      {
        float currentPoint = zombie.Get<ViewRef>().View.transform.position.x;
        Vector2 bounds = zombie.Get<PatrolBounds>().HorizontalBounds;
        float nextPoint = Random.Range(bounds.x, bounds.y);
        float direction = Mathf.Sign(nextPoint - currentPoint);
        
        _forceFactory.Create(new SpeedForceData(SpeedForceType.Move, zombie.PackedEntity, Vector2.right)
        {
          Speed = Vector2.right * _config.CalmSpeed,
          Direction = Vector2.right * direction
        });

        zombie
          .Del<PatrolCommand>()
          .Add<OnPatrolStarted>()
          .Add<Patrolling>()
          .Replace((ref MovePoint point) => point.Point = nextPoint);
      }
    }
  }
}