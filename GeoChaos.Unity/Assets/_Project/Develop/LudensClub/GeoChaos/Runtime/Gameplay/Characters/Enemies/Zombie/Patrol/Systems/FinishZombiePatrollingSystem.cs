using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Patrol;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Patrol
{
  public class FinishZombiePatrollingSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _patrollingZombies;

    public FinishZombiePatrollingSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _patrollingZombies = _game
        .Filter<ZombieTag>()
        .Inc<FinishPatrolCommand>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity zombie in _patrollingZombies)
      {
        zombie
          .Del<FinishPatrolCommand>()
          .Del<Patrolling>()
          .Add<OnPatrolFinished>();
      }
    }
  }
}