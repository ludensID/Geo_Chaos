using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Wait;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog
{
  public class FrogFeature : EcsFeature
  {
    public FrogFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<FrogWaitingFeature>());
    }
  }
}