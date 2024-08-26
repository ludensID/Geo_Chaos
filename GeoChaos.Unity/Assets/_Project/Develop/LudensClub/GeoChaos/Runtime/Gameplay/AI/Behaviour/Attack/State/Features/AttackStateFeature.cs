using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Attack.State
{
  public class AttackStateFeature<TFilterComponent> : EcsFeature
    where TFilterComponent : struct, IEcsComponent
  {
    public AttackStateFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<DeleteAttackStartedEventSystem<TFilterComponent>>());
      Add(systems.Create<AttackStateSystem<TFilterComponent>>());
      
      Add(systems.Create<DeleteAttackFinishedEventSystem<TFilterComponent>>());
      Add(systems.Create<FinishAttackStateSystem<TFilterComponent>>());
    }
  }
}