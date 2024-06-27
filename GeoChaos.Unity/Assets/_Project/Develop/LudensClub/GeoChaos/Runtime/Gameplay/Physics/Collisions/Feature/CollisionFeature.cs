using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Collisions
{
  public class CollisionFeature : EcsFeature
  {
    public CollisionFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<DeleteCollisionSystem>());
      Add(systems.Create<FlushCollisionsSystem>());
      Add(systems.Create<DamageFromDashSystem>());
      Add(systems.Create<DamageFromHeroAttackSystem>());
      Add(systems.Create<DamageFromShardSystem>());
      
    }
  }
}