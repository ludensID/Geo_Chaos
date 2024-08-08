using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public class SpeedForceFeature : EcsFeature
  {
    public SpeedForceFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<ConvertToSpeedForceSystem>());
      Add(systems.Create<DeleteNoImpactForcesSystem>());
      
      Add(systems.Create<DecreaseResidualForcesSystem>());
      
      Add(systems.Create<AccelerateSpeedsSystem>());
      Add(systems.Create<SetInstantZeroForcesSystem>());
      
      Add(systems.Create<CalculateTargetMovementVectorSystem>());
      Add(systems.Create<AssignLastMovementVectorSystem>());
    }
  }
}