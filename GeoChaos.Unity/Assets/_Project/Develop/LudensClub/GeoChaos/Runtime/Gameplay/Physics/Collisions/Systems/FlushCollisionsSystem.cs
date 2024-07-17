using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Collisions
{
  public class FlushCollisionsSystem : IEcsRunSystem
  {
    private readonly ICollisionFiller _filler;
    private readonly EcsWorld _message;

    public FlushCollisionsSystem(MessageWorldWrapper messageWorldWrapper, ICollisionFiller filler)
    {
      _filler = filler;
      _message = messageWorldWrapper.World;
    }

    public void Run(EcsSystems systems)
    {
      var collisions = _filler.Flush();
      for (int i = 0; i < collisions.Count; i++)
      {
        OneSideCollision other = collisions.Find(x => x.Type == collisions[i].Type && x.Sender.Collider == collisions[i].Other);
        if (other.Other == null)
        {
          CreateOneSideCollisionMessage(collisions[i]);
          continue;
        }

        CreateTwoSideCollisionMessage(new TwoSideCollision(collisions[i].Type, collisions[i].Sender, other.Sender));
        collisions.Remove(other);
      }
    }

    private EcsEntity CreateOneSideCollisionMessage(OneSideCollision collision)
    {
      return _message.CreateEntity()
        .Add<CollisionMessage>()
        .Add((ref OneSideCollision oneSideCollision) => oneSideCollision = collision);
    }
    
    private EcsEntity CreateTwoSideCollisionMessage(TwoSideCollision collision)
    {
      return _message.CreateEntity()
        .Add<CollisionMessage>()
        .Add((ref TwoSideCollision twoSideCollision) => twoSideCollision = collision);
    }
  }
}