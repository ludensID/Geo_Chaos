using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Move;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
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
        float viewDirX = bodyDir.Get<ViewDirection>().Direction.x;
        float direction = bodyDir.Has<FreeRotating>() && viewDirX != 0
          ? viewDirX
          : bodyDir.Get<MovementVector>().Direction.x;
        bodyDir.Replace((ref BodyDirection body) => body.Direction = direction);
      }
    }
  }
}