using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Immunity
{
  public class HeroImmunityFeature : EcsFeature
  {
    public HeroImmunityFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<TakeHeroImmunitySystem>());
      Add(systems.Create<DepriveHeroOfImmunitySystem>());
    }
  }
}