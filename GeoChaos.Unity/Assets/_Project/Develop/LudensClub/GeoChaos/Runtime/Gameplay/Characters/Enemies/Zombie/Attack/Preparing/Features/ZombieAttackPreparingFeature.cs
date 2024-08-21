using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Attack.Preparing
{
  public class ZombieAttackPreparingFeature : EcsFeature
  {
    public ZombieAttackPreparingFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<ZombieAttackPreparingSystem>());
      
      Add(systems.Create<DeleteZombieAttackPreparingFinishedEventSystem>());
      Add(systems.Create<FinishZombieAttackPreparingSystem>());
    }
  }
}