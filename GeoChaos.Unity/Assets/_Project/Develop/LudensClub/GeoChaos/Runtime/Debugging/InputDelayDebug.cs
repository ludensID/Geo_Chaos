#if UNITY_EDITOR
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Input;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Debugging
{
  public class InputDelayDebug : ITickable
  {
    private readonly EcsWorld _world;
    private readonly HeroConfig _config;
    private float _delay;

    public InputDelayDebug(InputWorldWrapper inputWorldWrapper, IConfigProvider configProvider)
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
        foreach (int input in _world.Filter<DelayedInput>().End())
        {
          _world.DelEntity(input);
        }
      }
    }
  }
}
#endif