using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero;
using LudensClub.GeoChaos.Runtime.Gameplay.View;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Gameplay.FlexibleCamera
{
  public class VerticalViewShiftSetter : ITickable
  {
    private readonly EcsEntity _hero;
    private readonly VirtualCameraModel _model;
    private readonly ITimerFactory _timers;
    private readonly CameraConfig _config;

    private float _target;
    private Timer _delay = 1;

    public VerticalViewShiftSetter(VirtualCameraModel model,
      IConfigProvider configProvider,
      ITimerFactory timers,
      IHeroHolder heroHolder)
    {
      _model = model;
      _timers = timers;
      _config = configProvider.Get<CameraConfig>();

      _hero = heroHolder.Hero;
    }

    public void Tick()
    {
      if (!TryGetShift(out float shift))
      {
        _delay.TimeLeft = _config.DelayBeforeLook + 1;
      }

      _model.VerticalViewShift.Value = shift;
    }

    private bool TryGetShift(out float shift)
    {
      shift = 0;
      if (!_hero.IsAlive())
        return false;

      float viewDirection = _hero.Get<ViewDirection>().Direction.y;
      if (Mathf.Abs(viewDirection) > 0.5f
        && _hero.Get<RigidbodyRef>().Rigidbody.velocity.magnitude.ApproximatelyEqual(0)
        && _hero.Get<MovementLayout>().Movement is MovementType.None)
      {
        if (_delay <= 0)
        {
          shift = viewDirection > 0 ? _config.LookUpShift : Mathf.Abs(_config.LookDownShift) * -1;
        }
        else if(_delay > _config.DelayBeforeLook)
        {
          _delay = _timers.Create(_config.DelayBeforeLook);
        }

        return true;
      }

      return false;
    }
  }
}