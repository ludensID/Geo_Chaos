using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public class CleanupFeature : EcsFeature
  {
    public CleanupFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<Delete<OnLanded, GameWorldWrapper>>());
      Add(systems.Create<Delete<OnLeftGround, GameWorldWrapper>>());
    }
  }
}