using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Bump;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Debugging.Watchers
{
  public class BumpAvailableWatcher : IWatcher
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _heroes;
    private readonly HeroConfig _config;
    private bool _bumpAvailable;

    public BumpAvailableWatcher(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _heroes = _game
        .Filter<HeroTag>()
        .Collect();

      _bumpAvailable = _config.EnableBump;
    }
      
    public bool IsDifferent()
    {
      return _bumpAvailable != _config.EnableBump;
    }

    public void Assign()
    {
      _bumpAvailable = _config.EnableBump;
    }

    public void OnChanged()
    {
      foreach (EcsEntity hero in _heroes)
      {
        hero.Has<BumpAvailable>(_bumpAvailable);
      }
    }
  }
}