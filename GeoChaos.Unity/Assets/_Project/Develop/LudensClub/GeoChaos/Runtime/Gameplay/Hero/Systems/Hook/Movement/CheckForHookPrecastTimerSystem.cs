using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Hook
{
  public class CheckForHookPrecastTimerSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _timers;

    public CheckForHookPrecastTimerSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _timers = _game
        .Filter<HookPrecast>()
        .Collect();
    } 
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity timer in _timers
        .Where<HookPrecast>(x => x.TimeLeft <= 0))
      {
        timer
          .Del<HookPrecast>()
          .Add<OnHookPrecastFinished>();
      }
    }
  }
}