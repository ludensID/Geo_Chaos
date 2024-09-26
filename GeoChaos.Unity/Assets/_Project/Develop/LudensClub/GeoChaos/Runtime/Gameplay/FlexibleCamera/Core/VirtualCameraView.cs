using Unity.Cinemachine;
using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Gameplay.FlexibleCamera
{
  [AddComponentMenu(ACC.Names.VIRTUAL_CAMERA_VIEW)]
  [RequireComponent(typeof(CinemachineCamera))]
  public class VirtualCameraView : MonoBehaviour
  {
    [HideInInspector]
    public CinemachineCamera Camera;

    [HideInInspector]
    public CinemachinePositionComposer Composer;

    [HideInInspector]
    public CinemachineShifter Shifter;

    [Inject]
    public void Construct()
    {
      Camera = GetComponent<CinemachineCamera>();
      Composer = GetComponent<CinemachinePositionComposer>();
      Shifter = GetComponent<CinemachineShifter>();
    }
  }
}