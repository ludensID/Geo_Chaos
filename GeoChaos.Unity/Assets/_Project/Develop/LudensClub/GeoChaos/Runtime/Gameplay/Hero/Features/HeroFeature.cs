using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class HeroFeature : EcsFeature
  {
    public HeroFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<CreateHeroEntitySystem>());
      
      Add(systems.Create<InitializeHeroMovementSystem>());
      
      Add(systems.Create<LockHeroMovementSystem>());
      Add(systems.Create<AddNextHeroMoveDirectionToQueueSystem>());
      Add(systems.Create<CalculateHeroVelocitySystem>());
      
      Add(systems.Create<CheckForHeroOnGroundSystem>());
      Add(systems.Create<CheckForHeroCanJumpSystem>());
      Add(systems.Create<ReadInputForHeroJumpSystem>());
      Add(systems.Create<CheckForHeroJumpStopSystem>());
      Add(systems.Create<CheckForHeroNeedStopJumpSystem>());
      Add(systems.Create<StopHeroJumpSystem>());
      
      Add(systems.Create<JumpHeroViewSystem>());
      Add(systems.Create<MoveHeroViewSystem>());
    }
  }
}