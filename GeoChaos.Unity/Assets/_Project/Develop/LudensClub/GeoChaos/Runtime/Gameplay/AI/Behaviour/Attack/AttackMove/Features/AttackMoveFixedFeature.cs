using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Attack.AttackMove
{
  public class AttackMoveFixedFeature<TFilterComponent> : EcsFeature
    where TFilterComponent : struct, IEcsComponent
  {
    public AttackMoveFixedFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<CheckForAttackMoveTimerExpiredSystem<TFilterComponent>>());
      Add(systems.Create<TurnEntityNearBoundsSystem<TFilterComponent>>());
    }
  }
}