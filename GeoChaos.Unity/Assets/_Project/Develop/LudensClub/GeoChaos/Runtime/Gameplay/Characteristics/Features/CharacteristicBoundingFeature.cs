using LudensClub.GeoChaos.Runtime.Gameplay.Characteristics.Endurance;
using LudensClub.GeoChaos.Runtime.Gameplay.Characteristics.Health;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characteristics
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