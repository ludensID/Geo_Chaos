using System;
using System.Collections.Generic;
using Unity.Cinemachine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.FlexibleCamera
{
  public class VirtualCameraManager : IVirtualCameraManager
  {
    private readonly List<CinemachineCamera> _stack = new List<CinemachineCamera>();

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
      _stack.Clear();
      SetCamera(_defaultCamera);
    }

    public void SetCamera(CinemachineCamera camera)
    {
      SetCameraInternal(camera, true);
    }

    public void UnsetCamera(CinemachineCamera camera)
    {
      int lastIndex = _stack.LastIndexOf(camera);
      if (lastIndex != -1)
        _stack.RemoveAt(lastIndex);

      if (camera == _mainCamera)
        SetCameraInternal(_stack[^1], false);
    }

    private void SetCameraInternal(CinemachineCamera camera, bool addToStack)
    {
      if (camera == _mainCamera)
        return;

      if (_mainCamera)
        _mainCamera.enabled = false;

      MainCamera = null;
      MainComposer = null;

      _mainCamera = camera;
      if (camera)
      {
        camera.enabled = true;
        MainComposer = camera.GetComponent<CinemachinePositionComposer>();
        if (addToStack)
          _stack.Add(camera);
      }

      MainCamera = camera;

      OnCameraChanged?.Invoke();
    }
  }
}