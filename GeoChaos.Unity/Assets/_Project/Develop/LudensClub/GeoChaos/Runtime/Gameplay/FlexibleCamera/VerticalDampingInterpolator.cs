using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Jump;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using Unity.Cinemachine;
using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Gameplay.FlexibleCamera
{
  public class VerticalDampingInterpolator : IVerticalDampingInterpolator, ITickable
  {
    private readonly EcsWorld _game;
    private readonly CameraConfig _config;
    private readonly EcsEntities _heroes;
    private readonly TweenerCore<float, float, FloatOptions> _fallTween;
    private readonly TweenerCore<float,float,FloatOptions> _backTween;

    private CinemachinePositionComposer _composer;
    private float _target;

    public VerticalDampingInterpolator(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<CameraConfig>();

      _heroes = _game
        .Filter<HeroTag>()
        .Collect();

      _fallTween = GetTween(_config.FallVerticalDamping)
        .SetAutoKill(false)
        .Pause();

      _backTween = GetTween(_config.DefaultVerticalDamping)
        .SetAutoKill(false)
        .Pause();
    }

    public void SetComposer(CinemachinePositionComposer composer)
    {
      _composer = composer;
    }

    private TweenerCore<float, float, FloatOptions> GetTween(float endValue)
    {
      return DOTween.To(GetDamping, SetDamping, endValue,
        _config.VerticalDampingInterpolationTime);
    }

    public void Tick()
    {
      foreach (EcsEntity hero in _heroes)
      {
        if (hero.Has<Falling>())
          TryPlayTween(_fallTween, _backTween);
        else
          TryPlayTween(_backTween, _fallTween);
      }
    }

    private void TryPlayTween(TweenerCore<float, float, FloatOptions> playTween, TweenerCore<float, float, FloatOptions> pauseTween)
    {
      if ((!playTween.IsActive() || !playTween.IsPlaying()) && !_target.ApproximatelyEqual(playTween.endValue))
      {
        pauseTween.Pause();
        _target = playTween.endValue;
        playTween.Restart();
        playTween.Play();
      }
    }

    private void SetDamping(float x)
    {
      Vector3 damping = _composer.Damping;
      damping.y = x;
      _composer.Damping = damping;
    }

    private float GetDamping()
    {
      return _composer.Damping.y;
    }
  }
}