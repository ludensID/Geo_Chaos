using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Attack.AttackMove;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Attack.Preparing;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Attack
{
  public class ZombieAttackFeature : EcsFeature
  {
    public ZombieAttackFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<ZombieAttackStateFeature>());
      Add(systems.Create<ZombieAttackCooldownFeature>());

      Add(systems.Create<ZombieAttackPreparingFeature>());
      Add(systems.Create<ZombieAttackMoveFeature>());

      Add(systems.Create<DamageFromZombieBodySystem>());
      Add(systems.Create<DamageFromZombieArmsSystem>());
    }
  }
}