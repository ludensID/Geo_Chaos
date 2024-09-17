using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Hook
{
  public class HeroHookFixedFeature : EcsFeature
  {
    public HeroHookFixedFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<DirectHeroHookSystem>());

      Add(systems.Create<CheckForHeroReachRingSystem>());
      Add(systems.Create<CheckForZeroHookForceSystem>());
      Add(systems.Create<CheckForHookTimerSystem>());
    }
  }
}