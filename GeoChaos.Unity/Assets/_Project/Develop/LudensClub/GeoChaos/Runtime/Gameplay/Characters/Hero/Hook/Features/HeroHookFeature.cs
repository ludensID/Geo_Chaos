﻿using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Hook
{
  public class HeroHookFeature : EcsFeature
  {
    public HeroHookFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<DeleteHookInputCooldownSystem>());
      Add(systems.Create<ReadHookInputSystem>());
      
      Add(systems.Create<ConvertDelayedToCurrentHookInputSystem>());
      Add(systems.Create<ReadHookDelayedInputSystem>());
      
      Add(systems.Create<CheckForSelectedRingSystem>());
      Add(systems.Create<MarkSelectedRingAsHookedSystem>());
      Add(systems.Create<HookSystem>());
      
      Add(systems.Create<Delete<OnHookPrecastStarted>>());
      Add(systems.Create<Delete<OnHookPrecastFinished>>());
      Add(systems.Create<PrecastHookSystem>());
      Add(systems.Create<CheckForHookPrecastTimerSystem>());
      
      Add(systems.Create<Delete<OnHookPullingStarted>>());
      Add(systems.Create<Delete<OnHookPullingFinished>>());
      Add(systems.Create<PullHeroOnHookSystem>());
      Add(systems.Create<StopHookPullingSystem>());
      
      Add(systems.Create<InterruptHookWhenHeroBumpSystem>());
      
      Add(systems.Create<Delete<OnHookInterrupted>>());
      Add(systems.Create<InterruptHookSystem>());
      Add(systems.Create<FinishHookInterruptionSystem>());
      
      Add(systems.Create<StopHookSystem>());
      
      Add(systems.Create<Delete<HookCommand>>());
    }
  }
}