using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Attack.AttackMove;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Attack.AttackMove
{
  public class ZombieAttackMoveFixedFeature : AttackMoveFixedFeature<ZombieTag>
  {
    public ZombieAttackMoveFixedFeature(IEcsSystemFactory systems) : base(systems)
    {
    }
  }
}