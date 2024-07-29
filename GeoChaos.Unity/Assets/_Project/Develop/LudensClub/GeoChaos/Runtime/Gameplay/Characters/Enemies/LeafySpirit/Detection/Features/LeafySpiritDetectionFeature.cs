using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Detection
{
  public class LeafySpiritDetectionFeature : EcsFeature
  {
    public LeafySpiritDetectionFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<AimLeafySpiritOnHeroSystem>());
    }
  }
}