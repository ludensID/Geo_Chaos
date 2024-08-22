using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Attack.AttackMove
{
  public class ZombieAttackMoveFeature : EcsFeature
  {
    public ZombieAttackMoveFeature(IEcsSystemFactory systems)
    {
        Add(systems.Create<ZombieAttackMovingSystem>());
        Add(systems.Create<FinishZombieAttackMovingSystem>());
    }
  }
}