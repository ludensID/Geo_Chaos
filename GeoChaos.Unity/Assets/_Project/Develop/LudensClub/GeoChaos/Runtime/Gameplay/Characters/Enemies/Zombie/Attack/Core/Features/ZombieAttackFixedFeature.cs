using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Attack.Preparing;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Attack
{
  public class ZombieAttackFixedFeature : EcsFeature
  {
    public ZombieAttackFixedFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<ZombieAttackPreparingFixedFeature>());
    }
  }
}