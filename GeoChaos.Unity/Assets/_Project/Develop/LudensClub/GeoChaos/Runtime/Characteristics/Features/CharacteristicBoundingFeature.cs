using LudensClub.GeoChaos.Runtime.Characteristics.Endurance;
using LudensClub.GeoChaos.Runtime.Characteristics.Health;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Characteristics
{
  public class CharacteristicBoundingFeature : EcsFeature
  {
    public CharacteristicBoundingFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<HealthBoundingFeature>());
      Add(systems.Create<EnduranceBoundingFeature>());
    }
  }
}