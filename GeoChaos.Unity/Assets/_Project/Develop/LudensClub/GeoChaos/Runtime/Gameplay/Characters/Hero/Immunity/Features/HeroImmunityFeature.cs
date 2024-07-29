using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Immunity
{
  public class HeroImmunityFeature : EcsFeature
  {
    public HeroImmunityFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<TakeHeroImmunitySystem>());
      Add(systems.Create<CheckHeroForImmunityWhileTimerSystem>());
      Add(systems.Create<DepriveHeroOfImmunitySystem>());
    }
  }
}