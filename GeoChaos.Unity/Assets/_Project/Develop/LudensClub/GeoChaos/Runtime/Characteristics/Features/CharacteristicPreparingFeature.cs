using LudensClub.GeoChaos.Runtime.Characteristics.Health;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Characteristics
{
  public class CharacteristicPreparingFeature : EcsFeature
  {
    public CharacteristicPreparingFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<HealthPreparingFeature>());
    }
  }
}