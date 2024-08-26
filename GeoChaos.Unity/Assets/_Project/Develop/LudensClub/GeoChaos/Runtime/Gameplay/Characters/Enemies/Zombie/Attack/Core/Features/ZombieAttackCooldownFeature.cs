using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Attack.Cooldown;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Attack
{
  public class ZombieAttackCooldownFeature : AttackCooldownFeature<ZombieTag>
  {
    public ZombieAttackCooldownFeature(IEcsSystemFactory systems) : base(systems)
    {
    }
  }
}