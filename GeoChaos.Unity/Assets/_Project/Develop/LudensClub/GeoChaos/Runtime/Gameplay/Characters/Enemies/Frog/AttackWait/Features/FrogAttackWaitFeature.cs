using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.AttackWait
{
  public class FrogAttackWaitFeature : EcsFeature
  {
    public FrogAttackWaitFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<FrogAttackWaitingSystem>());
      Add(systems.Create<DeleteExpiredFrogAttackWaitingTimerSystem>());
      Add(systems.Create<StopFrogAttackWaitingSystem>());
    }
  }
}