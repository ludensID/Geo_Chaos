using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.FlexibleCamera
{
  public class VerticalOffsetInterpolator : IDisposable
  {
    private readonly IVirtualCameraManager _manager;
    private readonly VirtualCameraModel _model;
    private readonly CameraConfig _config;
    
    private Tween _tweener;
    private float _target;

    public VerticalOffsetInterpolator(IVirtualCameraManager manager, VirtualCameraModel model, IConfigProvider configProvider)
    {
      _manager = manager;
      _model = model;
      _config = configProvider.Get<CameraConfig>();
      
      _model.EdgeVerticalOffset.OnChanged += UpdateOffset;
      _model.VerticalViewOffset.OnChanged += UpdateOffset;
    }

    public void Dispose()
    {
      _model.EdgeVerticalOffset.OnChanged -= UpdateOffset;
      _model.VerticalViewOffset.OnChanged -= UpdateOffset;
    }

    private void UpdateOffset()
    {
      float offset = 0;
      if (!_manager.MainComposer.Composition.DeadZone.Enabled)
        offset = _model.EdgeVerticalOffset;
      if (_model.VerticalViewOffset != 0)
        offset = _model.VerticalViewOffset;

      if (!_target.ApproximatelyEqual(offset))
      {
        _tweener?.Kill();
        _target = offset;
        _tweener = GetTween(_target);
      }
    }

    private TweenerCore<float, float, FloatOptions> GetTween(float endValue)
    {
      return DOTween.To(GetOffset, SetOffset, endValue, _config.VerticalOffsetInterpolationTime);
    }

    private void SetOffset(float x)
    {
      _model.VerticalOffset.Value = x;
    }

    private float GetOffset()
    {
      return _model.VerticalOffset;
    }
  }
}