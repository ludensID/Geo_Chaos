using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Gameplay.FlexibleCamera
{
  [AddComponentMenu(ACC.Names.PLAYER_CAMERA_LINKER)]
  public class PlayerCameraLinker : MonoBehaviour
  {
    [SerializeField]
    private VirtualCameraView _camera;

    [Inject]
    public void Construct(IVirtualCameraManager manager)
    {
      manager.SetDefaultCamera(_camera);
    }
  }
}