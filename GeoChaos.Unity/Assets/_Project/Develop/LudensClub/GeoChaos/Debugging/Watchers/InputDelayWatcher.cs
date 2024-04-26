using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Input;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;

namespace LudensClub.GeoChaos.Debugging.Watchers
{
  public class InputDelayWatcher : IWatcher
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

    public bool IsDifferent()
    {
      return _delay != _config.MovementResponseDelay;
    }

    public void Assign()
    {
      _delay = _config.MovementResponseDelay;
    }

    public void OnChanged()
    {
      foreach (var input in _world.Filter<DelayedInput>().End())
        _world.DelEntity(input);
    }
  }
}