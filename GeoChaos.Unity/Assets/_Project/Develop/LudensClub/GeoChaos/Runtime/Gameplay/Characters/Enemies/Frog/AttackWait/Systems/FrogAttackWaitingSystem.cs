using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.AttackWait
{
  public class FrogAttackWaitingSystem : IEcsRunSystem
  {
    private readonly ITimerFactory _timers;
    private readonly EcsWorld _game;
    private readonly FrogConfig _config;
    private readonly EcsEntities _waitingFrogs;

    public FrogAttackWaitingSystem(GameWorldWrapper gameWorldWrapper,
      IConfigProvider configProvider,
      ITimerFactory timers)
    {
      _timers = timers;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<FrogConfig>();

      _waitingFrogs = _game
        .Filter<FrogTag>()
        .Inc<WaitAttackCommand>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity frog in _waitingFrogs)
      {
        frog
          .Del<WaitAttackCommand>()
          .Add((ref AttackWaitingTimer timer) => timer.TimeLeft = _timers.Create(_config.HitCooldown));
      }
    }
  }
}