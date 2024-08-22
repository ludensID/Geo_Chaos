using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Watch
{
  public class ZombieWatchFixedFeature : EcsFeature
  {
    public ZombieWatchFixedFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<ZombieReachingWatchPointSystem>());
    }
  }
}