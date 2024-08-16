using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core.Destroying
{
  public class DestroyFeature : EcsFeature
  {
    public DestroyFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<DestroyAfterCollisionSystem>());
      
      Add(systems.Create<DestroyOwnedEntitiesSystem>());
      Add(systems.Create<DestroyEntitySystem>());
    }
  }
}