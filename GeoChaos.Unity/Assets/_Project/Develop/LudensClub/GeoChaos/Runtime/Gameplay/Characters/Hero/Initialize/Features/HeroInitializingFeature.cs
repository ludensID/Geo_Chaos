using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Initialize
{
  public class HeroInitializingFeature : EcsFeature
  {
    public HeroInitializingFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<InitializeHeroMovementSystem>());
      Add(systems.Create<InitializeHeroRigidbodySystem>());
    }
  }
}