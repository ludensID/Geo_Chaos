using Unity.Cinemachine;
using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Gameplay.FlexibleCamera
{
  [AddComponentMenu(ACC.Names.PLAYER_CAMERA_LINKER)]
  [RequireComponent(typeof(CinemachineCamera))]
  public class PlayerCameraLinker : MonoBehaviour
  {
    private CinemachineCamera _camera;

    [Inject]
    public void Construct(IPlayerCameraSetter setter)
    {
      _camera = GetComponent<CinemachineCamera>();
      setter.SetCamera(_camera);
    }
  }
}