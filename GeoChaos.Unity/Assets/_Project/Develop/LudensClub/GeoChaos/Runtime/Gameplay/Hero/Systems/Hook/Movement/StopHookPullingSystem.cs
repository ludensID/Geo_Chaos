using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Ring;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Hook
{
  public class StopHookPullingSystem : IEcsRunSystem
  {
    private readonly ITimerFactory _timers;
    private readonly EcsWorld _game;
    private readonly EcsEntities _stopCommands;
    private readonly HeroConfig _config;
    private readonly EcsEntities _hookedRings;

    public StopHookPullingSystem(GameWorldWrapper gameWorldWrapper, ITimerFactory timers, IConfigProvider configProvider)
    {
      _timers = timers;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _stopCommands = _game
        .Filter<StopHookPullingCommand>()
        .Inc<HookPulling>()
        .Collect();
      
      _hookedRings = _game
        .Filter<RingTag>()
        .Inc<Hooked>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity command in _stopCommands)
      {
        command
          .Del<HookPulling>()
          .Del<HookTimer>()
          .Del<StopHookPullingCommand>()
          .Add<OnHookPullingFinished>();
        
        foreach (EcsEntity ring in _hookedRings)
        {
          ring
            .Del<Hooked>()
            .Add((ref Releasing releasing) => releasing.TimeLeft = _timers.Create(_config.RingReleasingTime));
        }
      }
    }
  }
}