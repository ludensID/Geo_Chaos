using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Ring;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Hook
{
  public class CheckForRingReleasedSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _releasings;

    public CheckForRingReleasedSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _releasings = _game
        .Filter<Releasing>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity releasing in _releasings
        .Where<Releasing>(x => x.TimeLeft <= 0))
      {
        releasing.Del<Releasing>();
      }
    }
  }
}