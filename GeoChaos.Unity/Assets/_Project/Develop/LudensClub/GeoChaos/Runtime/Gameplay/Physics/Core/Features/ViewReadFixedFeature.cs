using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics
{
  public class ViewReadFixedFeature : EcsFeature
  {
    public ViewReadFixedFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<ReadViewVelocitySystem>());
    }
  }
}