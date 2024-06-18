using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Move;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems
{
  public class SetBodyDirectionSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _bodyDirections;

    public SetBodyDirectionSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _bodyDirections = _game
        .Filter<BodyDirection>()
        .Inc<ViewDirection>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity bodyDir in _bodyDirections)
      {
        float direction = CalculateBodyDirection(bodyDir);
        bodyDir.Replace((ref BodyDirection body) => body.Direction = direction);
      }
    }

    private static float CalculateBodyDirection(EcsEntity entity)
    {
      ref MovementVector vector = ref entity.Get<MovementVector>();
      float viewDirX = entity.Get<ViewDirection>().Direction.x;
      return (entity.Has<FreeRotating>() || vector.Direction.x * vector.Speed.x == 0) && viewDirX != 0
        ? viewDirX
        : vector.Direction.x;
    }
  }
}