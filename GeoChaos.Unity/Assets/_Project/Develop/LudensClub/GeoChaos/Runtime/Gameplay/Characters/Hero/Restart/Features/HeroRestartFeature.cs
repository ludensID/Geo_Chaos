using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Restart
{
  public class HeroRestartFeature : EcsFeature
  {
    public HeroRestartFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<DestroyHeroOnRestartSystem>());
      Add(systems.Create<FinishRestartHeroSystem>());
    }
  }
}