using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Retraction;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Collisions;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Leaf.Retraction
{
  public class CheckForRetractedLeafCollidedWithGroundSystem : IEcsRunSystem
  {
    private readonly ICollisionService _collisionSvc;
    private readonly EcsWorld _message;
    private readonly EcsWorld _game;
    private readonly PhysicsConfig _physics;
    private readonly EcsEntities _oneSideCollisions;

    public CheckForRetractedLeafCollidedWithGroundSystem(MessageWorldWrapper messageWorldWrapper,
      GameWorldWrapper gameWorldWrapper,
      ICollisionService collisionSvc,
      IConfigProvider configProvider)
    {
      _collisionSvc = collisionSvc;
      _message = messageWorldWrapper.World;
      _game = gameWorldWrapper.World;
      _physics = configProvider.Get<PhysicsConfig>();

      _oneSideCollisions = _message
        .Filter<OneSideCollision>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity col in _oneSideCollisions)
      {
        ref OneSideCollision collision = ref col.Get<OneSideCollision>();
        DamageCollisionInfo info = _collisionSvc.Info;
        _collisionSvc.AssignCollision(collision);
        if (_collisionSvc.UnpackEntities(_game)
          && info.Master.IsAlive()
          && info.Master.Has<LeafTag>()
          && !info.TargetCollider.Collider.isTrigger
          && _physics.GroundMask.Contains(info.TargetCollider.Collider.gameObject.layer)
          && info.Master.Has<Retracting>())
        {
          info.Master.Has<StopRetractCommand>(true);
        }
      }
    }
  }
}