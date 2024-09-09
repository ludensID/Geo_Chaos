using Unity.Cinemachine;
using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Gameplay.FlexibleCamera
{
  [RequireComponent(typeof(CinemachinePositionComposer))]
  [AddComponentMenu(ACC.Names.POSITION_COMPOSER_LINKER)]
  public class PositionComposerLinker : MonoBehaviour
  {
    private CinemachinePositionComposer _composer;

    [Inject]
    public void Construct(IVerticalDampingInterpolator interpolator)
    {
      _composer = GetComponent<CinemachinePositionComposer>();
      interpolator.SetComposer(_composer);
    }
  }
}