using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Correction;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Move
{
  public class FinishMoveLeafySpiritSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _movingSpirits;

    public FinishMoveLeafySpiritSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;
        
      _movingSpirits = _game
        .Filter<LeafySpiritTag>()
        .Inc<FinishMoveCommand>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity spirit in _movingSpirits)
      {
        spirit
          .Del<FinishMoveCommand>()
          .Del<Moving>()
          .Add<ShouldAttackCommand>();
      }
    }
  }
}