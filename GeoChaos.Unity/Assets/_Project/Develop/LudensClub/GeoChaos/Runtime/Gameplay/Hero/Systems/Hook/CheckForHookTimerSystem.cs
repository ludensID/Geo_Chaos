using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Hook
{
  public class CheckForHookTimerSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _hookTimers;

    public CheckForHookTimerSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _hookTimers = _game
        .Filter<HookTimer>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity timer in _hookTimers
        .Where<HookTimer>(timer => timer.TimeLeft <= 0))
      {
        timer.Add<StopHookCommand>();
        timer.Del<HookTimer>();
      }
    }
  }
}