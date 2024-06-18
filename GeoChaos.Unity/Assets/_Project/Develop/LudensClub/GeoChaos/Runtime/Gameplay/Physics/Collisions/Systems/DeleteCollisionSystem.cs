using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Collisions
{
  public class DeleteCollisionSystem : IEcsRunSystem
  {
    private readonly EcsWorld _message;
    private readonly EcsEntities _collisions;

    public DeleteCollisionSystem(MessageWorldWrapper messageWorldWrapper)
    {
      _message = messageWorldWrapper.World;

      _collisions = _message
        .Filter<CollisionMessage>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity col in _collisions)
        col.Dispose();
    }
  }
}