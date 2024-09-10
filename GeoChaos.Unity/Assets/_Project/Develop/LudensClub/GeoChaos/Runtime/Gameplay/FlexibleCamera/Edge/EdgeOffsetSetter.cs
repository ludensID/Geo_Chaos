using LudensClub.GeoChaos.Runtime.Configuration;

namespace LudensClub.GeoChaos.Runtime.Gameplay.FlexibleCamera
{
  public class EdgeOffsetSetter : IEdgeOffsetSetter
  {
    private readonly VirtualCameraModel _model;
    private readonly CameraConfig _config;

    public EdgeOffsetSetter(VirtualCameraModel model, IConfigProvider configProvider)
    {
      _model = model;
      _config = configProvider.Get<CameraConfig>();
    }

    public void SetEdgeOffset()
    {
      _model.EdgeVerticalOffset.Value = -_config.EdgeVerticalOffset;
    }

    public void SetDefaultOffset()
    {
      _model.EdgeVerticalOffset.Value = 0;
    }
  }
}