using Unity.Cinemachine;
using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Gameplay.FlexibleCamera
{
  [AddComponentMenu(ACC.Names.PLAYER_CAMERA_LINKER)]
  public class PlayerCameraLinker : MonoBehaviour
  {
    [SerializeField]
    private CinemachineCamera _camera;

    [Inject]
    public void Construct(IPlayerCameraSetter setter)
    {
      setter.SetCamera(_camera);
    }
  }
}