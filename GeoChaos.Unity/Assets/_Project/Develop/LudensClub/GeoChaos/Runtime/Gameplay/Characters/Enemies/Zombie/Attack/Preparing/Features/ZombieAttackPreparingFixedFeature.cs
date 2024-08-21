using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Attack.Preparing
{
  public class ZombieAttackPreparingFixedFeature : EcsFeature
  {
    public ZombieAttackPreparingFixedFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<ZombieReachingBackPointSystem>());
    }
  }
}