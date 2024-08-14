using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Bump
{
  public class FrogBumpFeature : EcsFeature
  {
    public FrogBumpFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<BumpFrogSystem>());
      Add(systems.Create<DeleteFrogBumpingSystem>());
    }
  }
}