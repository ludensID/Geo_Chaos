using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;
using Zenject;

namespace LudensClub.GeoChaos.Debugging.Watchers
{
  public class GravityScaleWatcher : IWatcher
  {
    private readonly EcsWorld _game;
    private readonly HeroConfig _config;
    private readonly EcsFilter _heroes;
    private float _gravityScale;

    public GravityScaleWatcher(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _gravityScale = _config.GravityScale;
      _heroes = _game
        .Filter<HeroTag>()
        .Inc<GravityScale>()
        .End();
    }

    public bool IsDifferent()
    {
      return _gravityScale != _config.GravityScale;
    }

    public void Assign()
    {
      _gravityScale = _config.GravityScale;
    }

    public void OnChanged()
    {
      if (!float.IsFinite(_gravityScale))
        return;

      foreach (int hero in _heroes)
      {
        ref GravityScale gravityScale = ref _game.Get<GravityScale>(hero);
        gravityScale.Value = _game.Has<Falling>(hero) ? _config.FallGravityScale : _config.GravityScale;
        gravityScale.Override = true;
      }
    }
  }
}