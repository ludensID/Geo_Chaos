using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characteristics.Health
{
  public class HealthPreparingFeature : EcsFeature
  {
    public HealthPreparingFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<DeleteSystem<OnHealthCalculated>>());
      Add(systems.Create<CalculateHealthSystem>());
    }
  }
}