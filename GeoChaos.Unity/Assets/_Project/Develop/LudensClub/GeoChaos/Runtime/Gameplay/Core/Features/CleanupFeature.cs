using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Jump;
using LudensClub.GeoChaos.Runtime.Gameplay.Core.Destroying;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Gravity;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class CleanupFeature : EcsFeature
  {
    public CleanupFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<Delete<OnLanded, GameWorldWrapper>>());
      Add(systems.Create<Delete<OnLifted, GameWorldWrapper>>());
      
      Add(systems.Create<DestroyFeature>());
    }
  }
}