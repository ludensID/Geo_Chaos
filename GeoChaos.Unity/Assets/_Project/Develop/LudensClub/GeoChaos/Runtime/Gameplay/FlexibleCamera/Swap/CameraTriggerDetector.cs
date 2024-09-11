using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Gameplay.FlexibleCamera.Swap
{
  [AddComponentMenu(ACC.Names.CAMERA_TRIGGER_DETECTOR)]
  public class CameraTriggerDetector : MonoBehaviour
  {
    [SerializeField]
    private VirtualCameraView _camera;
    
    private IVirtualCameraManager _manager;

    [Inject]
    public void Construct(IVirtualCameraManager manager)
    {
      _manager = manager;
    }
      
    private void OnTriggerEnter2D(Collider2D other)
    {
      if (other.attachedRigidbody.CompareTag("Player"))
      {
        _manager.SetCamera(_camera);
      }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
      if (other.attachedRigidbody.CompareTag("Player"))
      {
        _manager.UnsetCamera(_camera);
      }
    }
  }
}