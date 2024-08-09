using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Chase.Features
{
  public class FrogChasingFeature : EcsFeature
  {
    public FrogChasingFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<FrogChasingSystem>());
      Add(systems.Create<StopFrogChasingSystem>());
    }
  }
}