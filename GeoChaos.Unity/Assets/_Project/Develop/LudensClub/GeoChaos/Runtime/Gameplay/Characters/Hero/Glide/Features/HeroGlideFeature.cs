using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Glide
{
  public class HeroGlideFeature : EcsFeature
  {
    public HeroGlideFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<ResetHeroGlideOnGroundSystem>());
    }
  }
}