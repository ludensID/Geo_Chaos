using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Systems.Hook
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
        .Check<HookPrecast>(x => x.TimeLeft <= 0))
      {
        timer
          .Del<HookPrecast>()
          .Add<OnHookPrecastFinished>();
      }
    }
  }
}