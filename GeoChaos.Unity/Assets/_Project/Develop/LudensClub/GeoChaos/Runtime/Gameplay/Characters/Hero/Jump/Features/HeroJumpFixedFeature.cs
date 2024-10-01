using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Jump
{
  public class HeroJumpFixedFeature: EcsFeature
  {
    public HeroJumpFixedFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<ClampFallHeroVelocitySystem>());
    }
  }
}