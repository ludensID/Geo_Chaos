using LudensClub.GeoChaos.Runtime.Gameplay.Environment.Leaf;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment
{
  public class EnvironmentFixedFeature : EcsFeature
  {
    public EnvironmentFixedFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<LeafFixedFeature>());
    }
  }
}