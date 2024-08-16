using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Collisions;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core.Destroying
{
  public class DestroyAfterCollisionSystem : IEcsRunSystem
  {
    private readonly ICollisionService _collisionSvc;
    private readonly EcsEntities _twoCollisions;
    private readonly EcsWorld _message;
    private readonly EcsWorld _game;
    private readonly EcsEntities _oneCollisions;

    public DestroyAfterCollisionSystem(MessageWorldWrapper messageWorldWrapper,
      GameWorldWrapper gameWorldWrapper,
      ICollisionService collisionSvc)
    {
      _collisionSvc = collisionSvc;
      _message = messageWorldWrapper.World;
      _game = gameWorldWrapper.World;

      _twoCollisions = _message
        .Filter<TwoSideCollision>()
        .Collect();

      _oneCollisions = _message
        .Filter<OneSideCollision>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity col in _twoCollisions)
      {
        _collisionSvc.AssignCollision(col.Get<TwoSideCollision>());
        Destroy(_collisionSvc.Info);
      }

      foreach (EcsEntity collision in _oneCollisions)
      {
        _collisionSvc.AssignCollision(collision.Get<OneSideCollision>());
        Destroy(_collisionSvc.Info);
      }
    }

    private void Destroy(CollisionInfo info)
    {
      if (_collisionSvc.TryUnpackAnyEntity(_game)
        && _collisionSvc.TrySelectByMasterEntity(x => x.IsAlive() && x.Has<DestroyableAfterCollision>())
        && info.TargetCollider.Type != ColliderType.Action
        && (!info.Target.IsAlive() || !info.Master.Has<Owner>() 
          || !info.Master.Get<Owner>().Entity.EqualsTo(info.PackedTarget)))
      {
        info.Master.Has<DestroyCommand>(true);
      }
    }
  }
}