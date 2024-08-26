using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Patrol
{
  public class PatrolToRandomPointFeature<TFilterComponent> : EcsFeature
    where TFilterComponent : struct, IEcsComponent
  {
    public PatrolToRandomPointFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<PatrolToRandomPointSystem<TFilterComponent>>());
      
      Add(systems.Create<DeletePatrolFinishedEventSystem<TFilterComponent>>());
      Add(systems.Create<FinishPatrolSystem<TFilterComponent>>());
      
      Add(systems.Create<StopPatrolToRandomPointSystem<TFilterComponent>>());
    }
  }
}