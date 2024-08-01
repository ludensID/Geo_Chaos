using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Relaxation
{
  public class StopLeafySpiritRelaxationSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _relaxingSpirits;

    public StopLeafySpiritRelaxationSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _relaxingSpirits = _game
        .Filter<LeafySpiritTag>()
        .Inc<StopRelaxCommand>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity spirit in _relaxingSpirits)
      {
        spirit
          .Del<StopRelaxCommand>()
          .Has<Relaxing>(false)
          .Has<RelaxationTimer>(false);
      }
    }
  }
}