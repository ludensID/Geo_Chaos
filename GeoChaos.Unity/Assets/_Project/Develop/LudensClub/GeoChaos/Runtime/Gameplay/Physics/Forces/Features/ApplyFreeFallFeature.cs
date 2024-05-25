using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public class ApplyFreeFallFeature : EcsFeature
  {
    public ApplyFreeFallFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<ApplyDragForceSystem>());
      Add(systems.Create<DragSpeedForcesSystem>());
      
      Add(systems.Create<ApplyADControlSystem>());
    }
  }
}