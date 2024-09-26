using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Jump;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Gameplay.FlexibleCamera
{
  public class VerticalDampingInterpolator : IVerticalDampingInterpolator, ITickable
  {
    private readonly EcsEntity _hero;
    private readonly VirtualCameraModel _model;
    private readonly EcsWorld _game;
    private readonly CameraConfig _config;
    private readonly EcsEntities _heroes;
    private readonly TweenerCore<float, float, FloatOptions> _fallTween;
    private readonly TweenerCore<float, float, FloatOptions> _backTween;

    private float _target;

    public VerticalDampingInterpolator(IConfigProvider configProvider, VirtualCameraModel model, IHeroHolder heroHolder)
    {
      _model = model;
      _config = configProvider.Get<CameraConfig>();
      _hero = heroHolder.Hero;

      _fallTween = GetTween(_config.FallVerticalDamping);
      _backTween = GetTween(_config.DefaultVerticalDamping);

      _model.VerticalDamping.SetValueSilent(_config.DefaultVerticalDamping);
    }

    private TweenerCore<float, float, FloatOptions> GetTween(float endValue)
    {
      TweenerCore<float, float, FloatOptions> tween = DOTween.To(GetDamping, SetDamping, endValue,
          _config.VerticalDampingInterpolationTime)
        .SetAutoKill(false)
        .Pause();

      return tween;
    }

    private void SetDamping(float x)
    {
      _model.VerticalDamping.Value = x;
    }

    private float GetDamping()
    {
      return _model.VerticalDamping;
    }

    public void Tick()
    {
      if (_hero.IsAlive())
      {
        if (_hero.Has<Falling>())
          TryPlayTween(_fallTween, _backTween);
        else
          TryPlayTween(_backTween, _fallTween);
      }
    }

    private void TryPlayTween(TweenerCore<float, float, FloatOptions> playTween,
      TweenerCore<float, float, FloatOptions> pauseTween)
    {
      if (!playTween.IsPlaying() && !_target.ApproximatelyEqual(playTween.endValue))
      {
        pauseTween.Pause();
        _target = playTween.endValue;
        playTween.Restart();
      }
    }
  }
}