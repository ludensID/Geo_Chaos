using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Input;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using Zenject;

namespace LudensClub.GeoChaos.Debugging.Watchers
{
  public class InputDelayWatcher : ITickable
  {
    private readonly EcsWorld _world;
    private readonly HeroConfig _config;
    private float _delay;

    public InputDelayWatcher(InputWorldWrapper inputWorldWrapper, IConfigProvider configProvider)
    {
      _world = inputWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();
      _delay = _config.MovementResponseDelay;
    }

    public void Tick()
    {
      if (_delay != _config.MovementResponseDelay)
      {
        _delay = _config.MovementResponseDelay;
        foreach (var input in _world.Filter<DelayedInput>().End())
          _world.DelEntity(input);
      }
    }
  }
}