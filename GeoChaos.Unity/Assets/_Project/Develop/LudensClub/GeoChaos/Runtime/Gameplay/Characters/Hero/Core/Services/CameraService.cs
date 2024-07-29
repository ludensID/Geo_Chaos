using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero
{
  public class CameraService : ICameraService
  {
    private readonly Camera _camera;

    public CameraService(Camera camera)
    {
      _camera = camera;
    }

    public Vector3 ScreenToWorldPoint(Vector3 position)
    {
      return _camera.ScreenToWorldPoint(position);
    }

    public Vector3 WorldToScreenPoint(Vector3 position)
    {
      return _camera.WorldToScreenPoint(position);
    }
  }
}