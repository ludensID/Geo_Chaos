using DG.Tweening;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Gameplay.FlexibleCamera
{
  public class HeroRotationInterpolator : IHeroRotationInterpolator, ITickable, IFixedTickable
  {
    private readonly IHeroHolder _heroHolder;
    private readonly CameraConfig _config;
    private readonly EcsEntity _hero;
    
    private HeroFollower _heroFollower;
    
    private float _target;
    private Tweener _tweener;

    public HeroRotationInterpolator(IConfigProvider configProvider, IHeroHolder heroHolder)
    {
      _heroHolder = heroHolder;
      _config = configProvider.Get<CameraConfig>();
      _hero = _heroHolder.Hero;
    }

    public void SetFollower(HeroFollower follower)
    {
      _heroFollower = follower;
    }

    public void Tick()
    {
      if (_hero.IsAlive())
      {
        Transform heroTransform = GetHeroTransform();
        float currentTarget = heroTransform.rotation.eulerAngles.y;
        if (!_target.ApproximatelyEqual(currentTarget))
        {
          _tweener?.Kill();
          _tweener = _heroFollower.transform
            .DORotate(heroTransform.rotation.eulerAngles, _config.RotationTime)
            .SetUpdate(UpdateType.Fixed);
          _target = currentTarget;
        }
      }
    }

    public void FixedTick()
    {
      if(_hero.IsAlive())
        _heroFollower.Rb.MovePosition(GetHeroTransform().position);
    }

    private Transform GetHeroTransform()
    {
      return _hero.Get<ViewRef>().View.transform;
    }
  }
}