using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Immunity;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Hook;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Features
{
  public class HeroFixedFeature : EcsFeature
  {
    public HeroFixedFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<CheckForHeroReachRingSystem>());
      Add(systems.Create<CheckForZeroHookForceSystem>());
      Add(systems.Create<CheckForHookTimerSystem>());

      Add(systems.Create<DisableImmunityColliderSystem>());
      Add(systems.Create<EnableImmunityColliderSystem>());
    }
  }
}