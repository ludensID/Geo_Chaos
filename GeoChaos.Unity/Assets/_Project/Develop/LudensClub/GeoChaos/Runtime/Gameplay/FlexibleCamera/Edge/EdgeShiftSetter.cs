using LudensClub.GeoChaos.Runtime.Configuration;

namespace LudensClub.GeoChaos.Runtime.Gameplay.FlexibleCamera
{
  public class EdgeShiftSetter : IEdgeShiftSetter
  {
    private readonly VirtualCameraModel _model;
    private readonly CameraConfig _config;

    public EdgeShiftSetter(VirtualCameraModel model, IConfigProvider configProvider)
    {
      _model = model;
      _config = configProvider.Get<CameraConfig>();
    }

    public void SetEdgeOffset()
    {
      _model.EdgeVerticalShift.Value = -_config.EdgeVerticalShift;
    }

    public void SetDefaultOffset()
    {
      _model.EdgeVerticalShift.Value = 0;
    }
  }
}