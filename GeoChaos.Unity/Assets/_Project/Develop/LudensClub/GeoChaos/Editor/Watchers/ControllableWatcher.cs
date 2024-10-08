using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Editor.Watchers
{
  public class ControllableWatcher : IWatcher
  {
    private readonly Runtime.Configuration.HeroConfig _config;
    private readonly EcsEntities _heroes;
    private readonly EcsWorld _game;
    private bool _controllable;

    public ControllableWatcher(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<Runtime.Configuration.HeroConfig>();

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
        hero.Has<ADControllable>(_controllable);
        
        if (hero.Has<Hooking>())
          hero.Add<InterruptHookCommand>();
      }
    }
  }
}