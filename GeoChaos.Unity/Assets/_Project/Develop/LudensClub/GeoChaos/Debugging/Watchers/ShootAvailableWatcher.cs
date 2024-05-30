using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Shot;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Debugging.Watchers
{
  public class ShootAvailableWatcher : IWatcher
  {
    private readonly EcsWorld _game;
    private readonly HeroConfig _config;
    private readonly EcsEntities _heroes;
    private bool _shootAvailable;

    public ShootAvailableWatcher(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _heroes = _game
        .Filter<HeroTag>()
        .Collect();

      _shootAvailable = _config.EnableShoot;
    }
    
    public bool IsDifferent()
    {
      return _shootAvailable == _config.EnableShoot;
    }

    public void Assign()
    {
      _shootAvailable = _config.EnableShoot;
    }

    public void OnChanged()
    {
      foreach (EcsEntity hero in _heroes)
      {
        hero.Has<ShootAvailable>(_shootAvailable);
      }
    }
  }
}