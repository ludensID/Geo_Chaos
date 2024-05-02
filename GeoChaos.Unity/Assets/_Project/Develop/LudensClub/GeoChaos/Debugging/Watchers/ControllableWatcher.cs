using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Debugging.Watchers
{
  public class ControllableWatcher : IWatcher
  {
    private readonly HeroConfig _config;
    private readonly EcsEntities _heroes;
    private readonly EcsWorld _game;
    private bool _controllable;

    public ControllableWatcher(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _heroes = _game
        .Filter<HeroTag>()
        .Collect();

      _controllable = _config.EnableADControl;
    }
    
    public bool IsDifferent()
    {
      return _controllable != _config.EnableADControl;
    }

    public void Assign()
    {
      _controllable = _config.EnableADControl;
    }

    public void OnChanged()
    {
      foreach (EcsEntity hero in _heroes)
      {
        hero.Is<Controllable>(_controllable);
      }
    }
  }
}