using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.View;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies
{
  public class SetBodyDirectionByMovementSystem<TComponent> : IEcsRunSystem
    where TComponent : struct, IEcsComponent
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _entities;

    public SetBodyDirectionByMovementSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _entities = _game
        .Filter<TComponent>()
        .Inc<BodyDirection>()
        .Inc<MovementVector>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity entity in _entities)
      {
        ref MovementVector vector = ref entity.Get<MovementVector>();
        ref BodyDirection direction = ref entity.Get<BodyDirection>();
        if (vector.Speed.x * vector.Direction.x != 0)
          direction.Direction = vector.Direction.x;
      }
    }
  }
}