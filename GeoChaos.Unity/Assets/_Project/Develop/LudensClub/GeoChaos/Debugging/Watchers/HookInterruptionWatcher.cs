using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;
using Zenject;

namespace LudensClub.GeoChaos.Debugging.Watchers
{
  public class HookInterruptionWatcher : ITickable
  {
    private readonly EcsWorld _game;
    private readonly HeroConfig _config;
    private bool _allowHookInterruption;
    private readonly EcsEntities _heroes;


    public HookInterruptionWatcher(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _allowHookInterruption = _config.AllowHookInterruption;
      _heroes = _game
        .Filter<HeroTag>()
        .Collect();
    }

    public void Tick()
    {
      if (_allowHookInterruption != _config.AllowHookInterruption)
      {
        _allowHookInterruption = _config.AllowHookInterruption;
        foreach (EcsEntity hero in _heroes)
        {
          hero.Is<InterruptHookAvailable>(_allowHookInterruption);
        }
      }
    }
  }
}