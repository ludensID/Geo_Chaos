using System;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.FlexibleCamera
{
  public class MainCameraSyncer : IDisposable
  {
    private readonly IVirtualCameraManager _manager;
    private readonly VirtualCameraModel _model;

    public MainCameraSyncer(IVirtualCameraManager manager, VirtualCameraModel model)
    {
      _manager = manager;
      _model = model;

      _manager.OnCameraChanged += SyncCameraWithModel;
      _model.VerticalDamping.OnChanged += SyncCameraWithModel;
      _model.VerticalShift.OnChanged += SyncCameraWithModel;
    }

    public void Dispose()
    {
      _manager.OnCameraChanged -= SyncCameraWithModel;
      _model.VerticalDamping.OnChanged -= SyncCameraWithModel;
      _model.VerticalShift.OnChanged -= SyncCameraWithModel;
    }

    private void SyncCameraWithModel()
    {
      if (_manager.MainCamera.Composer)
      {
        _manager.MainCamera.Composer.Damping.SetY(_model.VerticalDamping);
      }

      if (_manager.MainCamera.Shifter)
      {
        _manager.MainCamera.Shifter.Shift = _model.VerticalShift;
      }
    }
  }
}