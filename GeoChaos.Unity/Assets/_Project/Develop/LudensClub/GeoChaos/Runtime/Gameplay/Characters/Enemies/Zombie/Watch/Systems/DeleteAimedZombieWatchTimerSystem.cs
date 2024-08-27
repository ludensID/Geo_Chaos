using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Watch;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.ArmsAttack;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Watch
{
  public class DeleteAimedZombieWatchTimerSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _watchingZombies;

    public DeleteAimedZombieWatchTimerSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _watchingZombies = _game
        .Filter<ZombieTag>()
        .Inc<WatchingTimer>()
        .Inc<Aimed>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity zombie in _watchingZombies)
      {
        zombie
            .Del<WatchingTimer>()
            .Has<StopAttackWithArmsCycleCommand>(true);
      }
    }
  }
}