using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Collisions
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
      var collisions = _filler.Flush();
      for (int i = 0; i < collisions.Count; i++)
      {
        OneSideCollision other = collisions.Find(x => x.Sender.Collider == collisions[i].Other);
        if (other.Other == null)
        {
          CreateOneSideCollisionMessage(collisions[i]);
          continue;
        }

        CreateTwoSideCollisionMessage(new TwoSideCollision(collisions[i].Sender, other.Sender));
        collisions.Remove(other);
      }
    }

    private EcsEntity CreateOneSideCollisionMessage(OneSideCollision collision)
    {
      EcsEntity entity = _world.CreateEntity();
      entity
        .Add<CollisionMessage>()
        .Add((ref OneSideCollision oneSideCollision) => oneSideCollision = collision);
      return entity;
    }
    
    private EcsEntity CreateTwoSideCollisionMessage(TwoSideCollision collision)
    {
      EcsEntity entity = _world.CreateEntity();
      entity
        .Add<CollisionMessage>()
        .Add((ref TwoSideCollision twoSideCollision) => twoSideCollision = collision);
      return entity;
    }
  }
}