using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Rise;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Relaxation
{
  public class FinishLeafySpiritRelaxationSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _relaxingSpirits;

    public FinishLeafySpiritRelaxationSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _relaxingSpirits = _game
        .Filter<LeafySpiritTag>()
        .Inc<RelaxationTimer>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity spirit in _relaxingSpirits
        .Check<RelaxationTimer>(x => x.TimeLeft <= 0))
      {
        spirit
          .Del<RelaxationTimer>()
          .Del<Relaxing>()
          .Del<Risen>()
          .Add<OnRelaxationFinished>();
      }
    }
  }
}