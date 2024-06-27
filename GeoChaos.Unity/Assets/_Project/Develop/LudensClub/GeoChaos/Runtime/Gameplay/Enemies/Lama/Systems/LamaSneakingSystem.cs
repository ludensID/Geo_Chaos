using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemies.Lama
{
  public class LamaSneakingSystem : IEcsRunSystem
  {
    private readonly ITimerFactory _timers;
    private readonly EcsWorld _game;
    private readonly EcsEntities _lamas;
    private readonly SpeedForceLoop _forceLoop;
    private readonly LamaConfig _config;

    public LamaSneakingSystem(GameWorldWrapper gameWorldWrapper,
      ITimerFactory timers,
      ISpeedForceLoopService forceLoopSvc,
      IConfigProvider configProvider)
    {
      _timers = timers;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<LamaConfig>();
      
      _forceLoop = forceLoopSvc.CreateLoop(x => 
        x.Exc<SpeedForceCommand>());

      _lamas = _game
        .Filter<LamaTag>()
        .Inc<SneakCommand>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity lama in _lamas)
      {
        if (!lama.Has<Sneaking>())
        {
          lama
            .Add<Sneaking>()
            .Add((ref SneakingTimer timer) => timer.TimeLeft = _timers.Create(_config.ListenTime));

          foreach (EcsEntity force in _forceLoop
            .GetLoop(SpeedForceType.Chase, lama.Pack()))
          {
            force.Replace((ref SpeedForce speedForce) => speedForce.Type = SpeedForceType.Sneak);
          }
        }

        lama.Del<SneakCommand>();
      }
    }
  }
}