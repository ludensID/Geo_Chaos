using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Shoot.Aim;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Debugging.Watchers
{
  public class AimAvailableWatcher : IWatcher
  {
    private readonly EcsWorld _game;
    private readonly HeroConfig _config;
    private readonly EcsEntities _heroes;
    private bool _aimAvailable;

    public AimAvailableWatcher(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _heroes = _game
        .Filter<HeroTag>()
        .Collect();

      _aimAvailable = _config.EnableAim;
    }

    public bool IsDifferent()
    {
      return _aimAvailable != _config.EnableAim;
    }

    public void Assign()
    {
      _aimAvailable = _config.EnableAim;
    }

    public void OnChanged()
    {
      foreach (EcsEntity hero in _heroes)
      {
        hero.Has<AimAvailable>(_aimAvailable);
      }
    }
  }
}