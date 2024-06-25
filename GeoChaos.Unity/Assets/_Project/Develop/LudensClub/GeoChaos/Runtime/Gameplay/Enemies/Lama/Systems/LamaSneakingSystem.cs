using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.AI;
using LudensClub.GeoChaos.Runtime.Gameplay.AI;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemies.Lama
{
  public class LamaSneakingSystem : IEcsRunSystem
  {
    private readonly ITimerFactory _timers;
    private readonly EcsWorld _game;
    private readonly EcsEntities _lamas;
    private readonly SpeedForceLoop _forceLoop;

    public LamaSneakingSystem(GameWorldWrapper gameWorldWrapper, ITimerFactory timers, ISpeedForceLoopService forceLoopSvc)
    {
      _timers = timers;
      _game = gameWorldWrapper.World;
      
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
          var ctx = lama.Get<BrainContext>().Cast<LamaContext>();
          lama
            .Add<Sneaking>()
            .Add((ref SneakingTimer timer) => timer.TimeLeft = _timers.Create(ctx.ListenTime));

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