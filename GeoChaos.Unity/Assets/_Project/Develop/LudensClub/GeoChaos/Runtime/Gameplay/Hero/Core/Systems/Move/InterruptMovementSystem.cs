using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Lock;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Move;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Move
{
  public class InterruptMovementSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _movings;

    public InterruptMovementSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _movings = _game
        .Filter<Moving>()
        .Inc<OnMovementLocked>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity moving in _movings)
      {
        moving.Del<Moving>();
      }
    }
  }
}