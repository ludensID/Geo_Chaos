using LudensClub.GeoChaos.Runtime.Gameplay.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Core.Dash;
using LudensClub.GeoChaos.Runtime.Gameplay.Enemies;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Bump;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.HealthShard;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Immunity;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Interaction;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Shoot.Aim;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Dash;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Jump;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Move;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Shoot;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Shoot.Aim;
using LudensClub.GeoChaos.Runtime.Gameplay.Ring;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Features
{
  public class HeroFeature : EcsFeature
  {
    public HeroFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<CreateHeroEntitySystem>());

      Add(systems.Create<InitializeHeroMovementSystem>());
      Add(systems.Create<InitializeHeroRigidbodySystem>());
      
      Add(systems.Create<HeroBumpFeature>());

      Add(systems.Create<ReadViewDirectionInputSystem>());

      Add(systems.Create<Delete<MoveCommand, GameWorldWrapper>>());
      Add(systems.Create<ReadMovementSystem>());
      Add(systems.Create<SowMoveCommandSystem>());
      Add(systems.Create<InterruptMovementSystem>());
      Add(systems.Create<CalculateHeroMovementVectorSystem>());
      
      Add(systems.Create<FallHeroSystem>());
      Add(systems.Create<LandHeroSystem>());

      Add(systems.Create<SetJumpHorizontalSpeedSystem>());

      Add(systems.Create<ReadInputForHeroJumpSystem>());
      Add(systems.Create<InterruptHeroJumpSystem>());
      Add(systems.Create<JumpHeroSystem>());
      Add(systems.Create<SowJumpStopCommandSystem>());
      Add(systems.Create<StopHeroJumpSystem>());

      Add(systems.Create<Delete<DashCommand, GameWorldWrapper>>());
      Add(systems.Create<Delete<StopDashCommand, GameWorldWrapper>>());
      Add(systems.Create<RemoveDashCooldownSystem>());
      Add(systems.Create<ReadDashInputSystem>());
      Add(systems.Create<ConvertDelayedToDashCommandSystem>());
      Add(systems.Create<ReadDashDelayedInputSystem>());
      Add(systems.Create<SowDashCommandSystem>());
      Add(systems.Create<DashHeroSystem>());
      Add(systems.Create<CheckForDashTimeExpiredSystem>());
      Add(systems.Create<StopHeroDashSystem>());
      
      Add(systems.Create<Delete<OnAttackStarted, GameWorldWrapper>>());
      Add(systems.Create<Delete<OnAttackFinished, GameWorldWrapper>>());
      Add(systems.Create<ReadAttackInputSystem>());
      Add(systems.Create<ResetComboCounterSystem>());
      Add(systems.Create<HeroAttackSystem>());
      Add(systems.Create<StopHeroAttackSystem>());

      Add(systems.Create<ReleaseRingSystem>());
      Add(systems.Create<CheckForRingReleasedSystem>());
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
      Add(systems.Create<StopHookPullingSystem>());
      
      Add(systems.Create<InterruptHookWhenHeroBumpSystem>());
      
      Add(systems.Create<Delete<OnHookInterrupted>>());
      Add(systems.Create<InterruptHookSystem>());
      Add(systems.Create<FinishHookInterruptionSystem>());
      
      Add(systems.Create<StopHookSystem>());
      
      Add(systems.Create<Delete<HookCommand>>());
      
      Add(systems.Create<SelectNearestDamagableEntitySystem>());

      Add(systems.Create<Delete<OnAimStarted>>());
      Add(systems.Create<Delete<OnAimFinished>>());
      Add(systems.Create<ReadAimInputSystem>());
      Add(systems.Create<CheckForOnGroundToAimSystem>());
      Add(systems.Create<SwitchAimSystem>());
      Add(systems.Create<Delete<StartAimCommand>>());
      Add(systems.Create<Delete<FinishAimCommand>>());
      
      Add(systems.Create<SetAimDirectionToViewDirectionSystem>());
      Add(systems.Create<ReadAimPositionSystem>());
      Add(systems.Create<ReadAimDirectionSystem>());
      Add(systems.Create<CheckForShootAndViewDirectionMatchingSystem>());
      
      Add(systems.Create<CheckForShootCooldownExpiredSystem>());
      Add(systems.Create<ReadShootInputSystem>());
      Add(systems.Create<SowShootCommandSystem>());
      Add(systems.Create<ShootSystem>());
      Add(systems.Create<CheckForShardLifeTimeExpiredSystem>());
      
      Add(systems.Create<HeroImmunityFeature>());

      Add(systems.Create<HeroInteractionFeature>());
      
      Add(systems.Create<HeroHealthShardFeature>());
      
      Add(systems.Create<SetHeroBodyDirectionSystem>());
      
      Add(systems.Create<HeroViewAttackSystem>());
      
      Add(systems.Create<DashHeroViewSystem>());
      Add(systems.Create<StopDashHeroViewSystem>());
      
      Add(systems.Create<SetHeroSwordViewColorSystem>());
    }
  }
}