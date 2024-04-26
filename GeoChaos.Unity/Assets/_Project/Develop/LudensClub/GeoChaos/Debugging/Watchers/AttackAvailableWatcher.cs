using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Debugging.Watchers
{
  public class AttackAvailableWatcher : IWatcher
  {
    private readonly EcsWorld _game;
    private readonly HeroConfig _config;
    private readonly EcsEntities _heroes;
    private bool _attackAvailable;

    public AttackAvailableWatcher(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _heroes = _game
        .Filter<HeroTag>()
        .Collect();

      _attackAvailable = _config.EnableAttack;
    }
    
    public bool IsDifferent()
    {
      return _attackAvailable != _config.EnableAttack;
    }

    public void Assign()
    {
      _attackAvailable = _config.EnableAttack;
    }

    public void OnChanged()
    {
      foreach (EcsEntity hero in _heroes)
        hero.Is<AttackAvailable>(_attackAvailable);
    }
  }
}