using Unity.Cinemachine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Gameplay.FlexibleCamera
{
  public class PlayerCameraSetter : IInitializable, IPlayerCameraSetter
  {
    private readonly IVirtualCameraManager _manager;
    private CinemachineCamera _camera;

    public PlayerCameraSetter(IVirtualCameraManager manager)
    {
      _manager = manager;
    }

    public void SetCamera(CinemachineCamera camera)
    {
      _camera = camera;
    }

    public void Initialize()
    {
      _manager.SetCamera(_camera);
    }
  }
}