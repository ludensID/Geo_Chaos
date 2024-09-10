using Unity.Cinemachine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Gameplay.FlexibleCamera
{
  public class PlayerCameraSetter : IInitializable, IPlayerCameraSetter
  {
    private readonly IVirtualCameraManager _manager;

    public PlayerCameraSetter(IVirtualCameraManager manager)
    {
      _manager = manager;
    }

    public void SetCamera(CinemachineCamera camera)
    {
      _manager.SetDefaultCamera(camera);
    }

    public void Initialize()
    {
      _manager.SetDefaultCamera();
    }
  }
}