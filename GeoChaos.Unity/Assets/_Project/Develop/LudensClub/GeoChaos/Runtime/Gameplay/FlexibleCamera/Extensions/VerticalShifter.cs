using Unity.Cinemachine;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.FlexibleCamera
{
  [AddComponentMenu(ACC.Names.VERTICAL_SHIFTER)]
  [ExecuteInEditMode]
  [DisallowMultipleComponent]
  [SaveDuringPlay]
  public class VerticalShifter : CinemachineExtension
  {
    public float Shift;
      
    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage,
      ref CameraState state, float deltaTime)
    {
      state.RawPosition += Vector3.up * Shift;
    }
  }
}