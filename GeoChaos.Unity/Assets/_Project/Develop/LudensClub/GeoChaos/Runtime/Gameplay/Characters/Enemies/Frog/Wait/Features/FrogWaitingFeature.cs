using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Wait
{
  public class FrogWaitingFeature : EcsFeature
  {
    public FrogWaitingFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<FrogWaitingSystem>());
      Add(systems.Create<FinishFrogWaitingSystem>());
      Add(systems.Create<StopFrogWaitingSystem>());
    }
  }
}