using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class DeleteOnGroundSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsFilter _onGrounds;
    private readonly EcsFilter _onNotGrounds;

    public DeleteOnGroundSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _onGrounds = _game
        .Filter<OnGround>()
        .End();

      _onNotGrounds = _game
        .Filter<OnNotGround>()
        .End();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (int onGround in _onGrounds)
      {
        _game.Del<OnGround>(onGround);
      }

      foreach (int onNotGround in _onNotGrounds)
      {
        _game.Del<OnNotGround>(onNotGround);
      }
    }
  }
}