using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Die
{
  public class DieFeature : EcsFeature
  {
    public DieFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<DestroyDiedEntitiesSystem>());
      Add(systems.Create<Delete<OnDied>>());
      Add(systems.Create<CheckForDieSystem>());
    }
  }
}