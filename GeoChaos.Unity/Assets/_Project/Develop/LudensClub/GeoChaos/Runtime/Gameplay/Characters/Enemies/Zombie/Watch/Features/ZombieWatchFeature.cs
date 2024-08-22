using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Watch
{
  public class ZombieWatchFeature : EcsFeature
  {
    public ZombieWatchFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<StartZombieWatchTimerSystem>());
      Add(systems.Create<DeleteExpiredZombieWatchTimerSystem>());
      Add(systems.Create<DeleteAimedZombieWatchTimerSystem>());
    }
  }
}