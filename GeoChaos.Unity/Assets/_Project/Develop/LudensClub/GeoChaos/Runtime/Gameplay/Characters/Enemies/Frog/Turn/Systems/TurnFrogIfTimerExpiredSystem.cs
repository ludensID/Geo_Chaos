using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.View;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Turn
{
  public class TurnFrogIfTimerExpiredSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _turningFrogs;

    public TurnFrogIfTimerExpiredSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _turningFrogs = _game
        .Filter<FrogTag>()
        .Inc<TurningTimer>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity frog in _turningFrogs
        .Check<TurningTimer>(x => x.TimeLeft <= 0))
      {
        frog
          .Del<TurningTimer>()
          .Change((ref BodyDirection direction) => direction.Direction *= -1);
      }
    }
  }
}