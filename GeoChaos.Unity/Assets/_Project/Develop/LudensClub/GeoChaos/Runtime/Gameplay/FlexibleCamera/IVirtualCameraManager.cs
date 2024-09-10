using System;
using Unity.Cinemachine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.FlexibleCamera
{
  public interface IVirtualCameraManager
  {
    ICinemachineCamera MainCamera { get; }
    CinemachinePositionComposer MainComposer { get; }
    event Action OnCameraChanged;
    void SetCamera(CinemachineCamera camera);
  }
}