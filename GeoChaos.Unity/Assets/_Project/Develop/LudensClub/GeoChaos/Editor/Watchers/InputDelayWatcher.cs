using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Input;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Editor.Watchers
{
  public class InputDelayWatcher : IWatcher
  {
    private readonly EcsWorld _input;
    private readonly Runtime.Configuration.HeroConfig _config;
    private float _delay;
    private readonly EcsEntities _inputs;

    public InputDelayWatcher(InputWorldWrapper inputWorldWrapper, IConfigProvider configProvider)
    {
      _input = inputWorldWrapper.World;
      _config = configProvider.Get<Runtime.Configuration.HeroConfig>();

      _inputs = _input
        .Filter<DelayedInput>()
        .Collect();
      
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
      foreach (EcsEntity input in _inputs)
        input.Dispose();
    }
  }
}