using LudensClub.GeoChaos.Runtime.Gameplay.Enemies.LeafySpirit.Leap;
using LudensClub.GeoChaos.Runtime.Gameplay.Enemies.LeafySpirit.Wait;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemies.LeafySpirit
{
  public class LeafySpiritFeature : EcsFeature
  {
    public LeafySpiritFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<LeafySpiritWaitFeature>());
      Add(systems.Create<LeafySpiritLeapFeature>());
    }
  }
}