using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Hook
{
  public class CheckForHookTimerSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _hookTimers;

    public CheckForHookTimerSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _hookTimers = _game
        .Filter<HeroTag>()
        .Inc<HookTimer>()
        .Exc<InterruptHookCommand>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity timer in _hookTimers
        .Check<HookTimer>(timer => timer.TimeLeft <= 0))
      {
        timer.Add<InterruptHookCommand>();
      }
    }
  }
}