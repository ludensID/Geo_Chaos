using LudensClub.GeoChaos.Runtime.Gameplay.Characteristics.Health;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characteristics
{
  public class CharacteristicPreparingFeature : EcsFeature
  {
    public CharacteristicPreparingFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<HealthPreparingFeature>());
    }
  }
}