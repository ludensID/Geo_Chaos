using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Shroom.Detection
{
  public class ShroomDetectionFeature : EcsFeature
  {
    public ShroomDetectionFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<AimShroomOnHeroSystem>());
    }
  }
}