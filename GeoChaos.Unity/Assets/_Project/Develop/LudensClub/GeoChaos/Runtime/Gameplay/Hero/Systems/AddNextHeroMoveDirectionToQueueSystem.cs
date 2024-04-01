using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class AddNextHeroMoveDirectionToQueueSystem : IEcsRunSystem
  {
    private readonly IInputDataProvider _input;
    private readonly ITimerService _timer;
    private readonly EcsWorld _world;
    private readonly EcsFilter _heroes;
    private readonly HeroConfig _config;

    public AddNextHeroMoveDirectionToQueueSystem(GameWorldWrapper gameWorldWrapper,
      IInputDataProvider input,
      IConfigProvider configProvider,
      ITimerService timer)
    {
      _input = input;
      _timer = timer;
      _world = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _heroes = _world.Filter<Hero>().Inc<Movable>().Inc<MovementQueue>().End();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (int move in _heroes)
      {
        var movable = _world.Get<Movable>(move);
        if (!movable.CanMove)
          continue;
        
        var delayedMovement = new DelayedMovement
        {
          WaitingTime = _config.MovementResponseDelay,
          Direction = _input.Data.HorizontalMovement
        };
        _timer.AddTimer(delayedMovement.WaitingTime);
        
        var movement = _world.Get<MovementQueue>(move);
        movement.NextMovements.Enqueue(delayedMovement);
      }
    }
  }
}