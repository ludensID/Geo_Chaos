using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public class FreeFallFeature : EcsFeature
  {
    public FreeFallFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<CreateDragForceSystem>());
      Add(systems.Create<DeleteDragForceSystem>());

      Add(systems.Create<CreateADControlSystem>());
      Add(systems.Create<DeleteADControlSystem>());

      Add(systems.Create<DiscardADControlSystem>());

      Add(systems.Create<PrepareFreeFallSystem>());
      Add(systems.Create<PrepareDragForceSystem>());
      Add(systems.Create<PrepareADControlSystem>());
      Add(systems.Create<DeleteSystem<OnActionStarted>>());
      
      Add(systems.Create<CheckForDelayExpiredSystem>());
      Add(systems.Create<StartFallFreeSystem>());

      Add(systems.Create<IncreaseGradientSystem>());

      Add(systems.Create<CalculateDragForceSystem>());
      Add(systems.Create<IgnoreMoveForcesSystem>());
      Add(systems.Create<StopIgnoreMoveForcesSystem>());
      Add(systems.Create<CalculateControlSpeedSystem>());

      Add(systems.Create<StopFreeFallOnGroundSystem>());
    }
  }
}