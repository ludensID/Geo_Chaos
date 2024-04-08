using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Attack.Components;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Systems
{
  public class DeleteCollisionSystem : IEcsRunSystem
  {
    private readonly EcsWorld _message;
    private readonly EcsFilter _collisions;

    public DeleteCollisionSystem(MessageWorldWrapper messageWorldWrapper)
    {
      _message = messageWorldWrapper.World;

      _collisions = _message
        .Filter<CollisionMessage>()
        .End();
    }

    public void Run(EcsSystems systems)
    {
      foreach (var col in _collisions)
        _message.DelEntity(col);
    }
  }
}