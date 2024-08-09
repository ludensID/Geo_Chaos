using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.JumpCycle
{
  public class FrogJumpCycleFeature : EcsFeature
  {
    public FrogJumpCycleFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<RestartFrogJumpCycleSystem>());
      Add(systems.Create<PrepareFrogJumpSystem>());
      Add(systems.Create<AfterFrogJumpSystem>());
      Add(systems.Create<StopFrogJumpCycleSystem>());
    }
  }
}