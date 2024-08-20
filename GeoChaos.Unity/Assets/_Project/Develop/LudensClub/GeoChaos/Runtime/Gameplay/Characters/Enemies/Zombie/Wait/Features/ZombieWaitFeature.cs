using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Wait
{
  public class ZombieWaitFeature : EcsFeature
  {
    public ZombieWaitFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<ZombieWaitingSystem>());
      Add(systems.Create<FinishZombieWaitingSystem>());
      Add(systems.Create<StopZombieWaitingSystem>());
    }
  }
}