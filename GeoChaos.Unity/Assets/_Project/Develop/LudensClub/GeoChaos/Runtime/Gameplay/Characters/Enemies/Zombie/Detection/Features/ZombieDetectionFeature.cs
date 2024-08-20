using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Detection
{
  public class ZombieDetectionFeature : EcsFeature
  {
    public ZombieDetectionFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<AimZombieOnHeroSystem>());
    }
  }
}