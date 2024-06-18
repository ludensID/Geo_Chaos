using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Ring
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
        releasing
          .Del<Releasing>()
          .Add<Selectable>();
      }
    }
  }
}