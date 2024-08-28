using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Attack.AttackMove;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Attack.AttackMove
{
  public class ZombieAttackMoveFeature : EcsFeature
  {
    public ZombieAttackMoveFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<StartZombieAttackMoveSystem>());
      Add(systems.Create<AttackMoveFeature<ZombieTag>>());
      Add(systems.Create<StopAttackWhenAttackMoveFinishedSystem>());
    }
  }
}