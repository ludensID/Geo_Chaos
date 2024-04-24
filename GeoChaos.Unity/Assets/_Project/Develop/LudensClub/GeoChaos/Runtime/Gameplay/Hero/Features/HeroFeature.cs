using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Core.Dash;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Lock;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.View;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Dash;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Jump;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.View;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Features
{
  public class HeroFeature : EcsFeature
  {
    public HeroFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<DeleteInitializeCommandForHeroSystem>());
      Add(systems.Create<CreateHeroEntitySystem>());

      Add(systems.Create<InitializeHeroMovementSystem>());
      Add(systems.Create<InitializeHeroRigidbodySystem>());

      Add(systems.Create<ReadHeroViewVelocitySystem>());

      Add(systems.Create<Delete<OnMovementLocked, GameWorldWrapper>>());
      Add(systems.Create<Delete<OnMovementUnlocked, GameWorldWrapper>>());
      Add(systems.Create<LockMovementSystem>());

      Add(systems.Create<ReadMovementSystem>());
      Add(systems.Create<CalculateHeroMovementVectorSystem>());
      Add(systems.Create<Delete<MoveCommand, GameWorldWrapper>>());

      Add(systems.Create<Delete<OnGround, GameWorldWrapper>>());
      Add(systems.Create<Delete<OnNotGround, GameWorldWrapper>>());
      Add(systems.Create<CheckForHeroOnGroundSystem>());
      Add(systems.Create<FallHeroSystem>());
      Add(systems.Create<LandHeroSystem>());

      Add(systems.Create<SetJumpHorizontalSpeedSystem>());

      Add(systems.Create<ReadInputForHeroJumpSystem>());
      Add(systems.Create<LockHeroJumpSystem>());
      Add(systems.Create<JumpHeroSystem>());
      Add(systems.Create<CheckForHeroJumpStopSystem>());
      Add(systems.Create<StopHeroJumpSystem>());

      Add(systems.Create<Delete<DashCommand, GameWorldWrapper>>());
      Add(systems.Create<Delete<StopDashCommand, GameWorldWrapper>>());
      Add(systems.Create<RemoveDashCooldownSystem>());
      Add(systems.Create<ReadDashInputSystem>());
      Add(systems.Create<ConvertDelayedToCurrentDashInputSystem>());
      Add(systems.Create<ReadDashDelayedInputSystem>());
      Add(systems.Create<CheckForHeroDashSystem>());
      Add(systems.Create<DashHeroSystem>());
      Add(systems.Create<CheckStopHeroDashSystem>());
      Add(systems.Create<StopHeroDashSystem>());
      
      Add(systems.Create<Delete<OnAttackStarted, GameWorldWrapper>>());
      Add(systems.Create<Delete<OnAttackFinished, GameWorldWrapper>>());
      Add(systems.Create<ReadAttackInputSystem>());
      Add(systems.Create<CheckAttackSystem>());
      Add(systems.Create<ResetComboCounterSystem>());
      Add(systems.Create<HeroAttackSystem>());
      Add(systems.Create<StopHeroAttackSystem>());

      Add(systems.Create<ReleaseRingsSystem>());
      Add(systems.Create<ClearSelectedRingsSystem>());
      Add(systems.Create<SelectRingsInHookRadiusSystem>());
      Add(systems.Create<SelectRingsDirectedToHeroSystem>());
      Add(systems.Create<SelectRingsInHeroViewSystem>());
      Add(systems.Create<SelectNearestRingSystem>());
      
      Add(systems.Create<DeleteHookInputCooldownSystem>());
      Add(systems.Create<ReadHookInputSystem>());
      
      Add(systems.Create<ConvertDelayedToCurrentHookInputSystem>());
      Add(systems.Create<ReadHookDelayedInputSystem>());
      
      Add(systems.Create<CheckForSelectedRingSystem>());
      Add(systems.Create<MarkSelectedRingAsHookedSystem>());
      Add(systems.Create<HookSystem>());
      
      Add(systems.Create<Delete<OnHookPrecastStarted, GameWorldWrapper>>());
      Add(systems.Create<Delete<OnHookPrecastFinished, GameWorldWrapper>>());
      Add(systems.Create<PrecastHookSystem>());
      Add(systems.Create<CheckForHookPrecastTimerSystem>());
      
      Add(systems.Create<Delete<OnHookPullingStarted, GameWorldWrapper>>());
      Add(systems.Create<Delete<OnHookPullingFinished, GameWorldWrapper>>());
      Add(systems.Create<PullHeroOnHookSystem>());
      
      Add(systems.Create<Delete<OnHookInterrupted, GameWorldWrapper>>());
      Add(systems.Create<InterruptHookSystem>());
      Add(systems.Create<FinishHookInterruptionSystem>());
      
      Add(systems.Create<StopHookPullingSystem>());
      Add(systems.Create<StopHookSystem>());
      
      Add(systems.Create<Delete<HookCommand, GameWorldWrapper>>());
      
      Add(systems.Create<CalculateHeroVelocitySystem>());
      
      Add(systems.Create<HeroViewAttackSystem>());
      
      Add(systems.Create<DashHeroViewSystem>());
      Add(systems.Create<StopDashHeroViewSystem>());

      Add(systems.Create<SetViewGravitySystem>());
      Add(systems.Create<SetHeroViewVelocitySystem>());
      Add(systems.Create<SetHeroViewRotationSystem>());
      Add(systems.Create<PrecastHookViewSystem>());
      
      Add(systems.Create<SetHeroSwordViewColorSystem>());
    }
  }
}