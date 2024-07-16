using LudensClub.GeoChaos.Runtime.Infrastructure.Spine;
using TriInspector;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Props.Environment.Bush
{
  [AddComponentMenu(ACC.Names.BUSH_ANIMATOR)]
  public class BushAnimator : MonoSpineAnimator<BushParameterType, BushAnimationType>
  {
    [Button("Set Trigger")]
    private void SetTrigger()
    {
      SetVariable(BushParameterType.Hit, true);
    }
  }
}