using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Wait
{
  public class WaitFeature<TFilterComponent> : EcsFeature
    where TFilterComponent : struct, IEcsComponent
  {
    public WaitFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<WaitSystem<TFilterComponent>>());
      Add(systems.Create<FinishWaitSystem<TFilterComponent>>());
      Add(systems.Create<StopWaitSystem<TFilterComponent>>());
    }
  }
}