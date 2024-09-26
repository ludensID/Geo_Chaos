using System;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.FlexibleCamera
{
  public class VerticalShiftUpdater : IDisposable
  {
    private readonly IVirtualCameraManager _manager;
    private readonly VirtualCameraModel _model;
    
    public VerticalShiftUpdater(IVirtualCameraManager manager, VirtualCameraModel model)
    {
      _manager = manager;
      _model = model;
      
      _model.EdgeVerticalShift.OnChanged += UpdateShift;
      _model.VerticalViewShift.OnChanged += UpdateShift;
      _model.AimShift.OnChanged += UpdateShift;
    }

    public void Dispose()
    {
      _model.EdgeVerticalShift.OnChanged -= UpdateShift;
      _model.VerticalViewShift.OnChanged -= UpdateShift;
      _model.AimShift.OnChanged -= UpdateShift;
    }

    private void UpdateShift()
    {
      Vector2 shift = Vector2.zero;
      if (!_manager.MainCamera.Composer.Composition.DeadZone.Enabled)
        shift.y = _model.EdgeVerticalShift;
      
      if (_model.VerticalViewShift != 0)
        shift.y = _model.VerticalViewShift;
      
      if(_model.AimShift != Vector2.zero)
        shift = _model.AimShift;
        
      _model.TargetShift.Value = shift;
    }
  }
}