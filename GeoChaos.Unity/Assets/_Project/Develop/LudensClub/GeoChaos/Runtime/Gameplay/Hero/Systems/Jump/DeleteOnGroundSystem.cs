using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class DeleteOnGroundSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsFilter _onGrounds;

    public DeleteOnGroundSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _onGrounds = _game
        .Filter<OnGround>()
        .End();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (var onGround in _onGrounds)
      {
        _game.Del<OnGround>(onGround);
      }
    }
  }
}