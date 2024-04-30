using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Attack.Components;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Props;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Systems
{
  public class FlushCollisionsSystem : IEcsRunSystem
  {
    private readonly ICollisionFiller _filler;
    private readonly EcsWorld _world;

    public FlushCollisionsSystem(MessageWorldWrapper messageWorldWrapper, ICollisionFiller filler)
    {
      _filler = filler;
      _world = messageWorldWrapper.World;
    }

    public void Run(EcsSystems systems)
    {
      foreach (var collision in _filler.Flush())
      {
        var collisionEntity = _world.NewEntity();
        _world.Add<CollisionMessage>(collisionEntity);
        ref var collisionComp = ref _world.Add<TwoSideCollision>(collisionEntity);
        collisionComp = collision;
      }
    }
  }
}