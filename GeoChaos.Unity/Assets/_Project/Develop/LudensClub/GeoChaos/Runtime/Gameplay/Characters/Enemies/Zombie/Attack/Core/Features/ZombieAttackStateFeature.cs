using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Attack.State;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Attack
{
  public class ZombieAttackStateFeature : AttackStateFeature<ZombieTag>
  {
    public ZombieAttackStateFeature(IEcsSystemFactory systems) : base(systems)
    {
    }
  }
}