using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.JumpWait
{
  public class FrogJumpWaitFeature : EcsFeature
  {
    public FrogJumpWaitFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<FrogJumpWaitSystem>());
      Add(systems.Create<DeleteFrogJumpWaitFinishedEventSystem>());
      Add(systems.Create<FinishFrogJumpWaitSystem>());
      Add(systems.Create<StopFrogJumpWaitSystem>());
    }
  }
}