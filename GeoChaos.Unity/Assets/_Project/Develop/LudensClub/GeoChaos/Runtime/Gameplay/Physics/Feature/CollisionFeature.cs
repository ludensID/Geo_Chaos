using LudensClub.GeoChaos.Runtime.Gameplay.Attack.Components;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Systems;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Feature
{
  public class CollisionFeature : EcsFeature
  {
    public CollisionFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<Delete<CollisionMessage, MessageWorldWrapper>>());
      Add(systems.Create<FlushCollisionsSystem>());
      Add(systems.Create<DamageFromDashSystem>());
    }
  }
}