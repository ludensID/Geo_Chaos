using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog
{
  public class FrogFixedFeature : EcsFeature
  {
    public FrogFixedFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<ResetToZeroFrogForceWhenLandingSystem>());
    }
  }
}