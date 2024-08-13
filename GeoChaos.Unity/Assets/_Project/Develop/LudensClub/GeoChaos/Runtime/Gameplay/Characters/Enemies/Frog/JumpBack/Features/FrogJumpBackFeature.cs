using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.JumpBack
{
  public class FrogJumpBackFeature : EcsFeature
  {
    public FrogJumpBackFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<FrogJumpBackSystem>());
      Add(systems.Create<FinishFrogJumpBackSystem>());
      Add(systems.Create<StopFrogJumpBackSystem>());
    }
  }
}