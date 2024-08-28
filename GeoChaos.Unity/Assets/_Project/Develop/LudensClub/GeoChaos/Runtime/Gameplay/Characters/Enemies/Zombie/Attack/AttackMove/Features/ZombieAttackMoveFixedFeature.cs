using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Attack.AttackMove;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Attack.AttackMove
{
  public class ZombieAttackMoveFixedFeature : EcsFeature
  {
    public ZombieAttackMoveFixedFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<CheckForZombieAttackMoveTimerExpiredSystem>());
      Add(systems.Create<AttackMoveFixedFeature<ZombieTag>>());
    }
  }
}