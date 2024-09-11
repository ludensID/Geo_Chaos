using System;

namespace LudensClub.GeoChaos.Runtime.Gameplay.FlexibleCamera
{
  public interface IVirtualCameraManager
  {
    VirtualCameraView MainCamera { get; }
    event Action OnCameraChanged;
    void SetDefaultCamera(VirtualCameraView camera);
    void SetDefaultCamera();
    void SetCamera(VirtualCameraView camera);
    void UnsetCamera(VirtualCameraView camera);
  }
}