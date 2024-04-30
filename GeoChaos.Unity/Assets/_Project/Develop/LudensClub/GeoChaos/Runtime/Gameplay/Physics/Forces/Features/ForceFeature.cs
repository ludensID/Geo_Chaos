using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.View;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public class ForceFeature : EcsFeature
  {
    public ForceFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<ReadViewVelocitySystem>());
      
      Add(systems.Create<ConvertToSpeedForceSystem>());
      Add(systems.Create<DeleteNoImpactForcesSystem>());
      Add(systems.Create<AccelerateSpeedsSystem>());
      Add(systems.Create<CalculateTargetVelocitySystem>());
      
      Add(systems.Create<CalculateVelocitySystem>());
      
      Add(systems.Create<SetViewVelocitySystem>());
    }
  }
}