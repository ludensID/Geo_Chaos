#if UNITY_EDITOR
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Debugging.Watchers
{
  public class GravityScaleWatcher : ITickable
  {
    private readonly HeroConfig _config;
    private readonly EcsWorld _world;
    private float _gravityScale;

    public GravityScaleWatcher(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _world = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();
      _gravityScale = _config.GravityScale;
    }
    
    public void Tick()
    {
      if (_gravityScale != _config.GravityScale)
      {
        _gravityScale = _config.GravityScale;
        foreach (int hero in _world.Filter<Hero>().Inc<RigidbodyRef>().End())
        {
          ref RigidbodyRef rigidbodyRef = ref _world.Get<RigidbodyRef>(hero);
          rigidbodyRef.Rigidbody.gravityScale = _config.GravityScale;
        }
      }
    }
  }
}
#endif