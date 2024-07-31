using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Retraction
{
  public class FinishLeafySpiritRetractionSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _retractingSpirits;

    public FinishLeafySpiritRetractionSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _retractingSpirits = _game
        .Filter<LeafySpiritTag>()
        .Inc<Retracting>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity spirit in _retractingSpirits)
      {
        if (!spirit.Has<Discharged>())
        {
          spirit
            .Del<Retracting>()
            .Add<OnRetractionFinished>();
        }
      }
    }
  }
}