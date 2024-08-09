using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Lama.Detection
{
  public class LamaDetectionFeature : EcsFeature
  {
    public LamaDetectionFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<AimLamaOnHeroSystem>());
      Add(systems.Create<AimLamaOnTargetInViewSystem>());
    }
  }
}