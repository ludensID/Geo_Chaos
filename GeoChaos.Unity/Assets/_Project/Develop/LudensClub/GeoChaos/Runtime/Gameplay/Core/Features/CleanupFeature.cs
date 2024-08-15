using LudensClub.GeoChaos.Runtime.Gameplay.Core.Destroying;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Gravity;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class CleanupFeature : EcsFeature
  {
    public CleanupFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<DeleteSystem<OnLanded>>());
      Add(systems.Create<DeleteSystem<OnLifted>>());
      
      Add(systems.Create<DestroyFeature>());
    }
  }
}