using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using LudensClub.GeoChaos.Runtime.Configuration;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.FlexibleCamera
{
  public class CameraShiftInterpolator : IDisposable
  {
    private readonly VirtualCameraModel _model;
    private readonly CameraConfig _config;

    private readonly TweenerCore<Vector2, Vector2, VectorOptions> _tweener;

    public CameraShiftInterpolator(VirtualCameraModel model, IConfigProvider configProvider)
    {
      _model = model;
      _config = configProvider.Get<CameraConfig>();

      _tweener = GetTween();
      _model.TargetShift.OnChanged += TweenShift;
    }

    public void Dispose()
    {
      _model.TargetShift.OnChanged += TweenShift;
    }

    private void TweenShift()
    {
      _tweener.ChangeValues(GetShift(), _model.TargetShift.Value);
      _tweener.Restart();
    }

    private TweenerCore<Vector2, Vector2, VectorOptions> GetTween()
    {
      return DOTween
        .To(GetShift, SetShift, GetShift(), _config.ShiftInterpolationTime)
        .SetAutoKill(false)
        .Pause();
    }

    private void SetShift(Vector2 vector)
    {
      _model.SmoothShift.Value = vector;
    }

    private Vector2 GetShift()
    {
      return _model.SmoothShift;
    }
  }
}