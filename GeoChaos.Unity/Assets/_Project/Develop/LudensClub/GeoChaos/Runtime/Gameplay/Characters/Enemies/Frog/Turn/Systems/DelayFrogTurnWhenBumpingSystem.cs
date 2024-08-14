using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Bump;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Turn
{
  public class DelayFrogTurnWhenBumpingSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _turningFrogs;

    public DelayFrogTurnWhenBumpingSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _turningFrogs = _game
        .Filter<FrogTag>()
        .Inc<FinishTurnCommand>()
        .Inc<Bumping>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity frog in _turningFrogs)
      {
        frog
          .Del<FinishTurnCommand>()
          .Add<DelayedTurn>();
      }
    }
  }
}