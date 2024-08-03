using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characteristics.Health
{
  public class HealthBoundingFeature : EcsFeature
  {
    public HealthBoundingFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<BoundCurrentHealthByMinSystem>());
      Add(systems.Create<BoundCurrentHealthByMaxSystem>());
    }
  }
}