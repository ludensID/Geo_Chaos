using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Debugging.Watchers
{
  public class DragForceAvailableWatcher : IWatcher
  {
    private readonly EcsWorld _game;
    private readonly HeroConfig _config;
    private readonly EcsEntities _heroes;
    private bool _dragForceAvailable;

    public DragForceAvailableWatcher(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _heroes = _game
        .Filter<HeroTag>()
        .Collect();

      _dragForceAvailable = _config.EnableDragForce;
    }
    
    public bool IsDifferent()
    {
      return _dragForceAvailable != _config.EnableDragForce;
    }

    public void Assign()
    {
      _dragForceAvailable = _config.EnableDragForce;
    }

    public void OnChanged()
    {
      foreach (EcsEntity hero in _heroes)
      {
        hero.Is<DragForceAvailable>(_dragForceAvailable);
      }
    }
  }
}