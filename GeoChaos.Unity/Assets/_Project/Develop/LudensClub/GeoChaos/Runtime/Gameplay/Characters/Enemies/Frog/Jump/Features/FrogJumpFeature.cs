using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Jump
{
  public class FrogJumpFeature : EcsFeature
  {
    public FrogJumpFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<DeleteFrogJumpStartedEventSystem>());
      Add(systems.Create<FrogJumpSystem>());

      Add(systems.Create<DeleteFrogJumpFinishedEventSystem>());
      Add(systems.Create<FinishFrogJumpSystem>());

      Add(systems.Create<StopFrogJumpSystem>());
    }
  }
}