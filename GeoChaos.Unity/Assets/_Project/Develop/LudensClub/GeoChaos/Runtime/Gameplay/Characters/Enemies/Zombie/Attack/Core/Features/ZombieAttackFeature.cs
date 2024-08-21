using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Attack.Preparing;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Attack
{
  public class ZombieAttackFeature : EcsFeature
  {
    public ZombieAttackFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<DeleteZombieAttackStartedEventSystem>());
      Add(systems.Create<ZombieAttackSystem>());
      
      Add(systems.Create<ZombieAttackPreparingFeature>());
    }
  }
}