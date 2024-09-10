using System;
using Unity.Cinemachine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.FlexibleCamera
{
  public class VirtualCameraManager : IVirtualCameraManager
  {
    private CinemachineCamera _defaultCamera;
    private CinemachineCamera _mainCamera;
    public ICinemachineCamera MainCamera { get; private set; }
    public CinemachinePositionComposer MainComposer { get; private set; }

    public event Action OnCameraChanged;

    public void SetDefaultCamera(CinemachineCamera camera)
    {
      _defaultCamera = camera;
    }

    public void SetDefaultCamera()
    {
      SetCamera(_defaultCamera);
    }

    public void SetCamera(CinemachineCamera camera)
    {
      if (camera == _mainCamera)
        return;

      if (_mainCamera)
        _mainCamera.enabled = false;

      MainCamera = null;
      MainComposer = null;

      _mainCamera = camera;
      if (camera)
        camera.enabled = true;

      MainCamera = camera;
      MainComposer = camera.GetComponent<CinemachinePositionComposer>();

      OnCameraChanged?.Invoke();
    }

    public void UnsetCamera(CinemachineCamera camera)
    {
      if (camera == _mainCamera)
        SetCamera(_defaultCamera);
    }
  }
}