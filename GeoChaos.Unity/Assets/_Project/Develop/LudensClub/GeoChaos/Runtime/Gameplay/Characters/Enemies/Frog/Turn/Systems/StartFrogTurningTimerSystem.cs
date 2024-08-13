using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.JumpWait;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Jump;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Damage;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Turn
{
  public class StartFrogTurningTimerSystem : IEcsRunSystem
  {
    private readonly ITimerFactory _timers;
    private readonly EcsWorld _game;
    private readonly EcsEntities _turningFrogs;
    private readonly FrogConfig _config;

    public StartFrogTurningTimerSystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider, ITimerFactory timers)
    {
      _timers = timers;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<FrogConfig>();

      _turningFrogs = _game
        .Filter<FrogTag>()
        .Inc<TurnCommand>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity frog in _turningFrogs)
      {
        if (!frog.Has<Attacking>() && !frog.Has<Jumping>() || frog.Has<JumpWaitTimer>())
        {
          frog
            .Del<TurnCommand>()
            .Add((ref TurningTimer timer) => timer.TimeLeft = _timers.Create(_config.TimeBeforeTurn));
        }
      }
    }
  }
}