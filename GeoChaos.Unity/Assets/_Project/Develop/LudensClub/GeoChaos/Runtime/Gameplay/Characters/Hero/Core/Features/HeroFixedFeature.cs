using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Bump;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Immunity;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Jump;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero
{
  public class HeroFixedFeature : EcsFeature
  {
    public HeroFixedFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<HeroJumpFixedFeature>());
      Add(systems.Create<HeroHookFixedFeature>());
      Add(systems.Create<HeroImmunityFixedFeature>());
      Add(systems.Create<HeroBumpFixedFeature>());
    }
  }
}