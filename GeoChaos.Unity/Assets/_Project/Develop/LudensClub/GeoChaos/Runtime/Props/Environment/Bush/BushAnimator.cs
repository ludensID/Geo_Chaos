using LudensClub.GeoChaos.Runtime.Infrastructure.Spine;
using TriInspector;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Props.Environment.Bush
{
  [AddComponentMenu(ACC.Names.BUSH_ANIMATOR)]
  public class BushAnimator : MonoSpineAnimator
  {
    private static readonly int _hit = "Hit".GetHashCode();
    private static readonly int _empty = "ToEmpty".GetHashCode();

    [Button("Set Hit")]
    private void SetHit()
    {
      SetTrigger(_hit);
    }
    
    [Button("Set Empty")]
    private void SetEmpty()
    {
      SetTrigger(_empty);
    }
  }
}