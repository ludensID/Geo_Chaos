using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Aim;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Shoot;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Gameplay.FlexibleCamera
{
  public class AimShiftSetter : ITickable
  {
    private readonly EcsEntity _hero;
    private readonly VirtualCameraModel _model;
    private readonly ITimerFactory _timers;
    private readonly CameraConfig _config;

    private float _target;
    private Timer _delay = 1;

    public AimShiftSetter(VirtualCameraModel model,
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
      if (!TryGetShift(out Vector2 shift))
      {
        _delay.TimeLeft = _config.DelayBeforeAim + 1;
      }

      _model.AimShift.Value = shift;
    }

    private bool TryGetShift(out Vector2 shift)
    {
      shift = Vector2.zero;
      if (!_hero.IsAlive())
        return false;

      if (_hero.Has<Aiming>())
      {
        if (_delay <= 0)
        {
          shift = _hero.Get<ShootDirection>().Direction.normalized * _config.AimShiftDistance;
        }
        else if(_delay > _config.DelayBeforeAim)
        {
          _delay = _timers.Create(_config.DelayBeforeAim);
        }

        return true;
      }

      return false;
    }
  }
}