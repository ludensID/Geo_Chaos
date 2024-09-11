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
      _model.VerticalOffset.OnChanged += SyncCameraWithModel;
    }

    public void Dispose()
    {
      _manager.OnCameraChanged -= SyncCameraWithModel;
      _model.VerticalDamping.OnChanged -= SyncCameraWithModel;
      _model.VerticalOffset.OnChanged -= SyncCameraWithModel;
    }

    private void SyncCameraWithModel()
    {
      if (_manager.MainCamera.Composer)
      {
        _manager.MainCamera.Composer.Damping.SetY(_model.VerticalDamping);
        _manager.MainCamera.Composer.TargetOffset.SetY(_model.VerticalOffset);
      }
    }
  }
}