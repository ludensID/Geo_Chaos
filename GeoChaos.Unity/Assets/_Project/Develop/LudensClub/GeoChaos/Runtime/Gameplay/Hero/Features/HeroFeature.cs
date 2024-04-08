using LudensClub.GeoChaos.Runtime.Gameplay.Core.Dash;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
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

      Add(systems.Create<ReadMovementSystem>());
      Add(systems.Create<CalculateHeroMovementVectorSystem>());
      Add(systems.Create<DeleteMoveCommandSystem>());

      Add(systems.Create<DeleteOnGroundSystem>());
      Add(systems.Create<CheckForHeroOnGroundSystem>());
      Add(systems.Create<FallHeroSystem>());
      Add(systems.Create<LandHeroSystem>());
      
      Add(systems.Create<SetJumpHorizontalSpeedSystem>());
      
      Add(systems.Create<ReadInputForHeroJumpSystem>());
      Add(systems.Create<JumpHeroSystem>());
      Add(systems.Create<CheckForHeroJumpStopSystem>());
      Add(systems.Create<StopHeroJumpSystem>());

      Add(systems.Create<DeleteDashCommandSystem>());
      Add(systems.Create<DeleteStopDashCommandSystem>());
      Add(systems.Create<RemoveDashCooldownSystem>());
      Add(systems.Create<ReadDashInputSystem>());
      Add(systems.Create<CheckForHeroDashSystem>());
      Add(systems.Create<DashHeroSystem>());
      Add(systems.Create<CheckStopHeroDashSystem>());
      Add(systems.Create<StopHeroDashSystem>());
      Add(systems.Create<DashHeroViewSystem>());
      Add(systems.Create<StopDashHeroViewSystem>());

      Add(systems.Create<CalculateHeroVelocitySystem>());

      Add(systems.Create<SetViewGravitySystem>());
      Add(systems.Create<SetHeroViewVelocitySystem>());
    }
  }
}