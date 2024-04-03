using LudensClub.GeoChaos.Runtime.Gameplay.Core.Dash;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class HeroFeature : EcsFeature
  {
    public HeroFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<CreateHeroEntitySystem>());

      Add(systems.Create<InitializeHeroMovementSystem>());
      
      Add(systems.Create<ReadHeroViewVelocitySystem>());

      Add(systems.Create<CheckHeroMovementSystem>());
      Add(systems.Create<AddNextHeroMoveDirectionToQueueSystem>());
      Add(systems.Create<ReadNextMovementSystem>());
      Add(systems.Create<CalculateHeroMovementVectorSystem>());
      Add(systems.Create<DeleteMoveCommandSystem>());

      Add(systems.Create<CheckForHeroOnGroundSystem>());
      Add(systems.Create<CheckForHeroCanJumpSystem>());
      Add(systems.Create<ReadInputForHeroJumpSystem>());
      Add(systems.Create<JumpHeroSystem>());
      Add(systems.Create<CheckForHeroJumpStopSystem>());
      Add(systems.Create<CheckForHeroNeedStopJumpSystem>());
      Add(systems.Create<StopHeroJumpSystem>());

      Add(systems.Create<DeleteDashCommandSystem>());
      Add(systems.Create<DeleteStopDashCommandSystem>());
      Add(systems.Create<ReadDashInputSystem>());
      Add(systems.Create<CheckForHeroDashSystem>());
      Add(systems.Create<DashHeroSystem>());
      Add(systems.Create<CheckStopHeroDashSystem>());
      Add(systems.Create<StopHeroDashSystem>());
      Add(systems.Create<DashHeroViewSystem>());
      Add(systems.Create<StopDashHeroViewSystem>());

      Add(systems.Create<CalculateHeroVelocitySystem>());
      
      Add(systems.Create<SetHeroViewVelocitySystem>());
    }
  }
}