using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.ArmsAttack
{
  public class ZombieArmsAttackFeature : EcsFeature
  {
    public ZombieArmsAttackFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<RestartAttackWithArmsCycleSystem>());

      Add(systems.Create<StartZombieAttackWithArmsCycleSystem>());

      Add(systems.Create<DeleteZombieExpiredAttackWithArmsCooldownSystem>());

      Add(systems.Create<DeleteZombieAttackWithArmsStartedEventSystem>());
      Add(systems.Create<StartZombieAttackWithArmsTimerSystem>());

      Add(systems.Create<DeleteZombieAttackWithArmsFinishedEventSystem>());
      Add(systems.Create<CheckForZombieAttackWithArmsTimerExpiredSystem>());

      Add(systems.Create<StopZombieAttackWithArmsCycleSystem>());

      Add(systems.Create<EnableArmsColliderSystem>());
    }
  }
}