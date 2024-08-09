using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.JumpWait
{
  public class FrogJumpWaitSystem : IEcsRunSystem
  {
    private readonly ITimerFactory _timers;
    private readonly EcsWorld _game;
    private readonly EcsEntities _waitingFrogs;
    private readonly FrogConfig _config;

    public FrogJumpWaitSystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider, ITimerFactory timers) 
    {
      _timers = timers;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<FrogConfig>();

      _waitingFrogs = _game
        .Filter<FrogTag>()
        .Inc<WaitJumpCommand>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity frog in _waitingFrogs)
      {
        frog
          .Del<WaitJumpCommand>()
          .Replace((ref JumpWaitTimer timer) => timer.TimeLeft = _timers.Create(_config.TimeAfterJump));
      }
    }
  }
}