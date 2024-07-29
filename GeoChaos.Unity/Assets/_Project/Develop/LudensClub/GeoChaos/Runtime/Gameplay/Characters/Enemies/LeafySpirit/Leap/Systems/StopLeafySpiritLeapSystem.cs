using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Leap
{
  public class StopLeafySpiritLeapSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _leapingSpirits;

    public StopLeafySpiritLeapSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _leapingSpirits = _game
        .Filter<LeafySpiritTag>()
        .Inc<StopLeapCommand>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity spirit in _leapingSpirits)
      {
        spirit
          .Del<StopLeapCommand>()
          .Has<Leaping>(false)
          .Has<PrecastLeapTimer>(false)
          .Has<LeapTimer>(false);
      }
    }
  }
}