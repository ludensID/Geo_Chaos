using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Patrol
{
  public class PatrolToRandomPointFixedFeature<TFilterComponent> : EcsFeature
    where TFilterComponent : struct, IEcsComponent
  {
    public PatrolToRandomPointFixedFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<ReachPatrolPointSystem<TFilterComponent>>());
    }
  }
}