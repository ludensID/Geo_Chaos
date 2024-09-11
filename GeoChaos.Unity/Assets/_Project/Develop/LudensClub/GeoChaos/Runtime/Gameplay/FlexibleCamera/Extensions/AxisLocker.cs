using LudensClub.GeoChaos.Runtime.Utils;
using Unity.Cinemachine;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.FlexibleCamera
{
  [AddComponentMenu(ACC.Names.AXIS_LOCKER)]
  [DisallowMultipleComponent]
  [ExecuteInEditMode]
  [SaveDuringPlay]
  public class AxisLocker : CinemachineExtension
  {
    public bool LockByHorizontal;
    public bool LockByVertical;

    private float _horizontalPosition;
    private float _verticalPosition;

    public override void PrePipelineMutateCameraStateCallback(CinemachineVirtualCameraBase vcam,
      ref CameraState curState, float deltaTime)
    {
      if (enabled)
      {
        if (LockByHorizontal)
        {
          _horizontalPosition = curState.RawPosition.x;
        }

        if (LockByVertical)
        {
          _verticalPosition = curState.RawPosition.y;
        }
      }
    }

    protected override void PostPipelineStageCallback(
      CinemachineVirtualCameraBase vcam,
      CinemachineCore.Stage stage,
      ref CameraState state,
      float deltaTime)
    {
      if (enabled && stage == CinemachineCore.Stage.Body)
      {
        if (LockByHorizontal)
        {
          state.RawPosition.SetX(_horizontalPosition);
        }

        if (LockByVertical)
        {
          state.RawPosition.SetY(_verticalPosition);
        }
      }
    }
  }
}