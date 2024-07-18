using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Characteristics
{
  public class CharacteristicPreparingFeature : EcsFeature
  {
    public CharacteristicPreparingFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<Delete<OnHealthCalculated, GameWorldWrapper>>());
      Add(systems.Create<CalculateHealthSystem>());
    }
  }
}