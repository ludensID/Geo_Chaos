using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Debugging.Watchers
{
  public class HookAvailableWatcher : IWatcher
  {
    private readonly EcsWorld _game;
    private readonly HeroConfig _config;
    private readonly EcsEntities _heroes;
    private bool _hookAvailable;

    public HookAvailableWatcher(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _heroes = _game
        .Filter<HeroTag>()
        .Collect();

      _hookAvailable = _config.EnableHook;
    }
    
    public bool IsDifferent()
    {
      return _hookAvailable != _config.EnableHook;
    }

    public void Assign()
    {
      _hookAvailable = _config.EnableHook;
    }

    public void OnChanged()
    {
      foreach (EcsEntity hero in _heroes)
        hero.Has<HookAvailable>(_hookAvailable);
    }
  }
}