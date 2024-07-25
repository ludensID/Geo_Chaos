using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemies.LeafySpirit.Leap
{
  public class FinishLeafySpiritLeapSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _leapingSpirits;

    public FinishLeafySpiritLeapSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _leapingSpirits = _game
        .Filter<LeafySpiritTag>()
        .Inc<LeapTimer>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity spirit in _leapingSpirits
        .Check<LeapTimer>(x => x.TimeLeft <= 0))
      {
        spirit
          .Del<LeapTimer>()
          .Del<Leaping>()
          .Add<OnLeapFinished>();
      }
    }
  }
}