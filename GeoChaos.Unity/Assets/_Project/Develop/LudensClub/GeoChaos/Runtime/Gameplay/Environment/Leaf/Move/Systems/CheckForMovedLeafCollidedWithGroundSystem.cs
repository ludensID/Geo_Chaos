using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Move;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Collisions;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Leaf.Move
{
  public class CheckForMovedLeafCollidedWithGroundSystem : IEcsRunSystem
  {
    private readonly ICollisionService _collisionSvc;
    private readonly EcsWorld _message;
    private readonly EcsWorld _game;
    private readonly PhysicsConfig _physics;
    private readonly EcsEntities _oneSideCollisions;

    public CheckForMovedLeafCollidedWithGroundSystem(MessageWorldWrapper messageWorldWrapper,
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
        CollisionInfo info = _collisionSvc.Info;
        _collisionSvc.AssignCollision(collision);
        if (_collisionSvc.TryUnpackByMasterEntity(_game)
          && info.Master.Has<LeafTag>()
          && !info.TargetCollider.Collider.isTrigger
          && _physics.GroundMask.Contains(info.TargetCollider.Collider.gameObject.layer)
          && info.Master.Has<Moving>())
        {
          info.Master.Has<StopMoveCommand>(true);
        }
      }
    }
  }
}