using DG.Tweening;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Gameplay.FlexibleCamera
{
  public class HeroRotationInterpolator : IHeroRotationInterpolator, IHeroBindable, ITickable, IFixedTickable
  {
    private readonly CameraConfig _config;
    
    private HeroFollower _heroFollower;
    private Transform _heroTransform;
    
    private float _target;
    private Tweener _tweener;

    public bool IsBound { get; set; }

    public HeroRotationInterpolator(IConfigProvider configProvider)
    {
      _config = configProvider.Get<CameraConfig>();
    }

    public void SetFollower(HeroFollower follower)
    {
      _heroFollower = follower;
    }

    public void BindHero(EcsEntity hero)
    {
      _heroTransform = hero.Get<ViewRef>().View.transform;
    }

    public void Tick()
    {
      if (!IsBound)
        return;
        
      float currentTarget = _heroTransform.rotation.eulerAngles.y;
      if (!_target.ApproximatelyEqual(currentTarget))
      {
        _tweener?.Kill();
        _tweener = _heroFollower.transform
          .DORotate(_heroTransform.rotation.eulerAngles, _config.RotationTime)
          .SetUpdate(UpdateType.Fixed);
        _target = currentTarget;
      }
    }

    public void FixedTick()
    {
      if (!IsBound)
        return;

      _heroFollower.Rb.MovePosition(_heroTransform.position);
    }
  }
}