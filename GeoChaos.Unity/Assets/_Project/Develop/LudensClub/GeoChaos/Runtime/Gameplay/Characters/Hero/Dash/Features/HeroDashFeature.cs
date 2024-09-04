using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Dash
{
  public class HeroDashFeature : EcsFeature
  {
    public HeroDashFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<DeleteSystem<DashCommand>>());
      Add(systems.Create<DeleteSystem<StopDashCommand>>());
      Add(systems.Create<RemoveDashCooldownSystem>());
      Add(systems.Create<ReadDashInputSystem>());
      Add(systems.Create<ConvertDelayedToJustComponentSystem<DelayDashCommand, DashCommand>>());
      Add(systems.Create<ReadDashDelayedInputSystem>());
      Add(systems.Create<SowDashCommandSystem>());
      Add(systems.Create<DashHeroSystem>());
      Add(systems.Create<CheckForDashTimeExpiredSystem>());
      Add(systems.Create<StopHeroDashSystem>());
      Add(systems.Create<DamageFromDashSystem>());
        
      Add(systems.Create<DashHeroViewSystem>());
      Add(systems.Create<StopDashHeroViewSystem>());
    }
  }
}