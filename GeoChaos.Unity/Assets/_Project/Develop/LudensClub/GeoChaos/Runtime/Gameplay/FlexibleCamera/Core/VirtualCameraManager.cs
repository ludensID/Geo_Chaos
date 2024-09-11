using System;
using System.Collections.Generic;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Gameplay.FlexibleCamera
{
  public class VirtualCameraManager : IVirtualCameraManager, IInitializable
  {
    private readonly List<VirtualCameraView> _views;
    private readonly List<VirtualCameraView> _stack = new List<VirtualCameraView>();

    private VirtualCameraView _defaultCamera;
    private VirtualCameraView _mainCamera;

    public VirtualCameraView MainCamera => _mainCamera;

    public event Action OnCameraChanged;

    public VirtualCameraManager(List<VirtualCameraView> views)
    {
      _views = views;
    }

    public void Initialize()
    {
      if (_defaultCamera == null)
        _defaultCamera = _views.Find(x => x.Camera.enabled);

      foreach (VirtualCameraView view in _views)
        view.Camera.enabled = false;
      
      SetDefaultCamera();
    }

    public void SetDefaultCamera(VirtualCameraView camera)
    {
      _defaultCamera = camera;
    }

    public void SetDefaultCamera()
    {
      _stack.Clear();
      SetCamera(_defaultCamera);
    }

    public void SetCamera(VirtualCameraView camera)
    {
      SetCameraInternal(camera, true);
    }

    public void UnsetCamera(VirtualCameraView camera)
    {
      int lastIndex = _stack.LastIndexOf(camera);
      if (lastIndex != -1)
        _stack.RemoveAt(lastIndex);

      if (camera == _mainCamera)
        SetCameraInternal(_stack[^1], false);
    }

    private void SetCameraInternal(VirtualCameraView camera, bool addToStack)
    {
      if (camera == _mainCamera)
        return;

      if (_mainCamera)
        _mainCamera.Camera.enabled = false;

      _mainCamera = camera;
      if (camera)
      {
        camera.Camera.enabled = true;
        if (addToStack)
          _stack.Add(camera);
      }

      OnCameraChanged?.Invoke();
    }
  }
}