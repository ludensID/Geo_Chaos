using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Leap
{
  public class StartLeafySpiritLeapSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _leapingSpirits;

    public StartLeafySpiritLeapSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _leapingSpirits = _game
        .Filter<LeafySpiritTag>()
        .Inc<PrecastLeapTimer>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity spirit in _leapingSpirits
        .Check<PrecastLeapTimer>(x => x.TimeLeft <= 0))
      {
        spirit
          .Del<PrecastLeapTimer>()
          .Add<LeapPoint>();
      }
    }
  }
}