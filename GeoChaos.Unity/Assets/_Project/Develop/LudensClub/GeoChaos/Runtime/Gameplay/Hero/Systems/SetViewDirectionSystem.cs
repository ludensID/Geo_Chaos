using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Move;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems
{
  public class SetViewDirectionSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _viewDirections;

    public SetViewDirectionSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _viewDirections = _game
        .Filter<ViewDirection>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity viewDir in _viewDirections)
      {
        viewDir.Replace((ref ViewDirection view) =>
        {
          view.Direction.x = viewDir.Has<FreeRotating>() && viewDir.Has<MoveCommand>()
            ? viewDir.Get<MoveCommand>().Direction
            : viewDir.Get<MovementVector>().Direction.x;
        });
      }
    }
  }
}