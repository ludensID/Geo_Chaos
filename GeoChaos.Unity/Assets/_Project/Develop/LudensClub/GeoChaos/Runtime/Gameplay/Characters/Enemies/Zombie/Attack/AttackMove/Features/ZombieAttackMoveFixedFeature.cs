using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Attack.AttackMove
{
  public class ZombieAttackMoveFixedFeature : EcsFeature
  {
    public ZombieAttackMoveFixedFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<CheckForAttackMoveTimerExpiredSystem>());
      Add(systems.Create<TurnZombieNearBoundsSystem>());
    }
  }
}