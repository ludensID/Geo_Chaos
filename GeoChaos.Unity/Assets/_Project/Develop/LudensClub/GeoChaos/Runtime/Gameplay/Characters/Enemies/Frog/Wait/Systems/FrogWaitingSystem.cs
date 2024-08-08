using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Wait;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Wait
{
  public class FrogWaitingSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly FrogConfig _config;
    private readonly EcsEntities _waitingFrogs;
    private readonly ITimerFactory _timers;

    public FrogWaitingSystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider, ITimerFactory timers)
    {
      _timers = timers;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<FrogConfig>();

      _waitingFrogs = _game
        .Filter<FrogTag>()
        .Inc<WaitCommand>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity frog in _waitingFrogs)
      {
        frog
          .Del<WaitCommand>()
          .Add((ref WaitingTimer timer) => timer.TimeLeft = _timers.Create(_config.WaitTime));
      }
    }
  }
}