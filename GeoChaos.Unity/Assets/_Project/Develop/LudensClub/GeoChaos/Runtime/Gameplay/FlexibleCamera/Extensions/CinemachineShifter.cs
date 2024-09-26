using Unity.Cinemachine;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.FlexibleCamera
{
  [AddComponentMenu(ACC.Names.CINEMACHINE_SHIFTER)]
  [ExecuteInEditMode]
  [DisallowMultipleComponent]
  [SaveDuringPlay]
  public class CinemachineShifter : CinemachineExtension
  {
    public Vector2 Shift;

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage,
      ref CameraState state, float deltaTime)
    {
      if (stage == CinemachineCore.Stage.Body)
      {
        state.RawPosition += (Vector3)Shift;
      }
    }
  }
}