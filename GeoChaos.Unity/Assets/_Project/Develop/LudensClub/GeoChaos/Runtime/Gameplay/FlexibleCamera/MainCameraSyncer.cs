using System;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.FlexibleCamera
{
  public class MainCameraSyncer : IMainCameraSyncer, IDisposable
  {
    private readonly IVirtualCameraManager _manager;
    private readonly VirtualCameraModel _model;

    public MainCameraSyncer(IVirtualCameraManager manager, VirtualCameraModel model)
    {
      _manager = manager;
      _model = model;

      _manager.OnCameraChanged += SyncCameraWithModel;
      _model.VerticalDamping.OnChanged += SyncCameraWithModel;
      _model.EdgeVerticalOffset.OnChanged += SyncCameraWithModel;
    }

    public void Dispose()
    {
      _manager.OnCameraChanged -= SyncCameraWithModel;
      _model.VerticalDamping.OnChanged -= SyncCameraWithModel;
      _model.EdgeVerticalOffset.OnChanged -= SyncCameraWithModel;
    }

    public void SyncCameraWithModel()
    {
      if (_manager.MainComposer)
      {
        _manager.MainComposer.Damping.SetY(_model.VerticalDamping);        
        _manager.MainComposer.TargetOffset.SetY(_model.EdgeVerticalOffset);
      }
    }
  }
}