using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class CameraProvider : ICameraProvider
  {
    public Camera Camera { get; }

    public CameraProvider(Camera camera)
    {
      Camera = camera;
    }
  }
}