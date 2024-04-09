using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;
using Zenject;

namespace LudensClub.GeoChaos.Debugging.Watchers
{
  public class GravityScaleWatcher : ITickable
  {
    private readonly EcsWorld _game;
    private readonly HeroConfig _config;
    private float _gravityScale;


    public GravityScaleWatcher(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _game = gameWorldWrapper.World; 
      _config = configProvider.Get<HeroConfig>();

      _gravityScale = _config.GravityScale;
    }
    
    public void Tick()
    {
      if (_gravityScale != _config.GravityScale)
      {
        _gravityScale = _config.GravityScale;
        if (!float.IsFinite(_gravityScale))
          return;
        
        foreach (int hero in _game.Filter<Hero>().Inc<GravityScale>().End())
        {
          ref GravityScale gravityScale = ref _game.Get<GravityScale>(hero);
          gravityScale.Value = _game.Has<IsFalling>(hero) ? _config.FallGravityScale : _config.GravityScale;
          gravityScale.Override = true;
        }
      }
    }
  }
}