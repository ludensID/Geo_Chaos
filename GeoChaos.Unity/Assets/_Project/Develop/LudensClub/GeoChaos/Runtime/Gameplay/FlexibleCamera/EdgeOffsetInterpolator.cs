using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using LudensClub.GeoChaos.Runtime.Configuration;

namespace LudensClub.GeoChaos.Runtime.Gameplay.FlexibleCamera
{
  public class EdgeOffsetInterpolator : IEdgeOffsetInterpolator
  {
    private readonly VirtualCameraModel _model;
    private readonly CameraConfig _config;
    private readonly TweenerCore<float,float,FloatOptions> _edgeTween;
    private readonly TweenerCore<float,float,FloatOptions> _backTween;

    public EdgeOffsetInterpolator(VirtualCameraModel model, IConfigProvider configProvider)
    {
      _model = model;
      _config = configProvider.Get<CameraConfig>();

      _edgeTween = GetTween(-_config.EdgeVerticalOffset);
      _backTween = GetTween(0);
    }

    private TweenerCore<float, float, FloatOptions> GetTween(float endValue)
    {
      TweenerCore<float, float, FloatOptions> tween = DOTween.To(GetOffset, SetOffset, endValue,
          _config.EdgeVerticalOffsetInterpolationTime)
        .SetAutoKill(false)
        .Pause();

      return tween;
    }

    private void SetOffset(float x)
    {
      _model.EdgeVerticalOffset.Value = x;
    }

    private float GetOffset()
    {
      return _model.EdgeVerticalOffset;
    }

    public void SetEdgeOffset()
    {
      SwitchTween(_edgeTween, _backTween);
    }

    public void SetDefaultOffset()
    {
      SwitchTween(_backTween, _edgeTween);
    }

    private void SwitchTween(Tween playTween, Tween pauseTween)
    {
      pauseTween.Pause();
      playTween.Restart();
    }
  }
}