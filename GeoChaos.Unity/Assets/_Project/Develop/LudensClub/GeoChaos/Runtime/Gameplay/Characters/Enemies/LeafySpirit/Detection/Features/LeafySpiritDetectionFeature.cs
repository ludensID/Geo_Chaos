using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Detection
{
  public class LeafySpiritDetectionFeature : EcsFeature
  {
    public LeafySpiritDetectionFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<DeleteLeafySpiritWasAimedSystem>());
      Add(systems.Create<RememberAimedLeafySpiritSystem>());
      Add(systems.Create<AimLeafySpiritOnHeroSystem>());
    }
  }
}