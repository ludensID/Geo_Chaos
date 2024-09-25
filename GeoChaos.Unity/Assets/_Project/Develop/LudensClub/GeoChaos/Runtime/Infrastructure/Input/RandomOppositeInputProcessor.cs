using LudensClub.GeoChaos.Runtime.Configuration;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public class RandomOppositeInputProcessor : IRandomOppositeInputProcessor
  {
    private readonly ITimerFactory _timers;

    private float _lastInput;
    private Timer _timer = 0;
    private readonly InputConfig _config;

    public RandomOppositeInputProcessor(ITimerFactory timers, IConfigProvider configProvider)
    {
      _timers = timers;
      _config = configProvider.Get<InputConfig>();
    }

    public float Process(float input)
    {
      if (input != 0 && _lastInput != 0 && input * _lastInput < 0 && _timer > 0)
        return 0;

      _lastInput = input;
      _timer = input != 0 ? _timers.Create(_config.OppositeHorizontalInputDelay, true) : 0;
      return input;
    }
  }
}