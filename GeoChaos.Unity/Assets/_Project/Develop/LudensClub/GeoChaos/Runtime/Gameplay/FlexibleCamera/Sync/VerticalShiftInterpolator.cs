using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.FlexibleCamera
{
  public class VerticalShiftInterpolator : IDisposable
  {
    private readonly IVirtualCameraManager _manager;
    private readonly VirtualCameraModel _model;
    private readonly CameraConfig _config;
    
    private Tween _tweener;
    private float _target;

    public VerticalShiftInterpolator(IVirtualCameraManager manager, VirtualCameraModel model, IConfigProvider configProvider)
    {
      _manager = manager;
      _model = model;
      _config = configProvider.Get<CameraConfig>();
      
      _model.EdgeVerticalShift.OnChanged += UpdateShift;
      _model.VerticalViewShift.OnChanged += UpdateShift;
    }

    public void Dispose()
    {
      _model.EdgeVerticalShift.OnChanged -= UpdateShift;
      _model.VerticalViewShift.OnChanged -= UpdateShift;
    }

    private void UpdateShift()
    {
      float shift = 0;
      if (!_manager.MainCamera.Composer.Composition.DeadZone.Enabled)
        shift = _model.EdgeVerticalShift;
      
      if (_model.VerticalViewShift != 0)
        shift = _model.VerticalViewShift;

      if (!_target.ApproximatelyEqual(shift))
      {
        _tweener?.Kill();
        _target = shift;
        _tweener = GetTween(_target);
      }
    }

    private TweenerCore<float, float, FloatOptions> GetTween(float endValue)
    {
      return DOTween.To(GetOffset, SetOffset, endValue, _config.VerticalOffsetInterpolationTime);
    }

    private void SetOffset(float x)
    {
      _model.VerticalShift.Value = x;
    }

    private float GetOffset()
    {
      return _model.VerticalShift;
    }
  }
}