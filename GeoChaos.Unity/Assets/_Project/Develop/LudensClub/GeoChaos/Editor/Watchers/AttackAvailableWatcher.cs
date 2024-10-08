using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Editor.Watchers
{
  public class AttackAvailableWatcher : IWatcher
  {
    private readonly EcsWorld _game;
    private readonly Runtime.Configuration.HeroConfig _config;
    private readonly EcsEntities _heroes;
    private bool _attackAvailable;

    public AttackAvailableWatcher(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<Runtime.Configuration.HeroConfig>();

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
        hero.Has<AttackAvailable>(_attackAvailable);
    }
  }
}