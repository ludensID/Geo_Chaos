using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Attack.AttackMove
{
  public class AttackMoveFeature<TFilterComponent> : EcsFeature
    where TFilterComponent : struct, IEcsComponent
  {
    public AttackMoveFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<AttackMovingSystem<TFilterComponent>>());
      Add(systems.Create<FinishAttackMovingSystem<TFilterComponent>>());
    }
  }
}