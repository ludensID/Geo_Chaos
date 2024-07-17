using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Collisions;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.DoorKey
{
  public class TakeKeyByHeroSystem : IEcsRunSystem
  {
    private readonly ICollisionService _collisionSvc;
    private readonly EcsWorld _message;
    private readonly EcsWorld _game;
    private readonly EcsEntities _collisions;

    public TakeKeyByHeroSystem(MessageWorldWrapper messageWorldWrapper,
      GameWorldWrapper gameWorldWrapper,
      ICollisionService collisionSvc)
    {
      _collisionSvc = collisionSvc;
      _message = messageWorldWrapper.World;
      _game = gameWorldWrapper.World;

      _collisions = _message
        .Filter<TwoSideCollision>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity col in _collisions)
      {
        ref TwoSideCollision collision = ref col.Get<TwoSideCollision>();
        DamageCollisionInfo info = _collisionSvc.Info;
        _collisionSvc.AssignCollision(collision);
        if (_collisionSvc.TryUnpackEntities(_game)
          && _collisionSvc.TrySelectByEntitiesTag<KeyTag, HeroTag>()
          && info.MasterCollider.Type == ColliderType.Action
          && info.TargetCollider.Type is ColliderType.Body or ColliderType.Dash)
        {
          info.Master.Add((ref Owner owner) => owner.Entity = info.PackedTarget);
          info.Master.Get<ConverterRef>().Converter.ConvertBackAndDestroy(info.Master);
        }

        _collisionSvc.Reset();
      }
    }
  }
}