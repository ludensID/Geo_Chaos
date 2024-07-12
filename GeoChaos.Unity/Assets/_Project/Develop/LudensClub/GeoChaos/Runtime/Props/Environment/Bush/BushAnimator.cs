using LudensClub.GeoChaos.Runtime.Infrastructure.Spine;
using TriInspector;

namespace LudensClub.GeoChaos.Runtime.Props.Environment.Bush
{
  public class BushAnimator : MonoSpineAnimator<BushParameterType, BushAnimationType>
  {
    [Button("Set Trigger")]
    private void SetTrigger()
    {
      SetVariable(BushParameterType.Hit, true);
    }
  }
}