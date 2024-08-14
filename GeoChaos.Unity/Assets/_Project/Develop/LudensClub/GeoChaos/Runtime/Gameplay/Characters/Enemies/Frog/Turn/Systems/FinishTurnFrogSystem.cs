using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.View;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Turn
{
  public class FinishTurnFrogSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _turningFrogs;

    public FinishTurnFrogSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _turningFrogs = _game
        .Filter<FrogTag>()
        .Inc<FinishTurnCommand>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity frog in _turningFrogs)
      {
        frog
          .Del<FinishTurnCommand>()
          .Change((ref BodyDirection bodyDirection) => bodyDirection.Direction *= -1);
      }
    }
  }
}