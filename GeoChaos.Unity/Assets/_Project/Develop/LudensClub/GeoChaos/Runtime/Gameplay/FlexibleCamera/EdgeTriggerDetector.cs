using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Gameplay.FlexibleCamera
{
  [AddComponentMenu(ACC.Names.EDGE_TRIGGER_DETECTOR)]
  public class EdgeTriggerDetector : MonoBehaviour
  {
    private IEdgeOffsetInterpolator _interpolator;

    [Inject]
    public void Construct(IEdgeOffsetInterpolator interpolator)
    {
      _interpolator = interpolator;
    }
      
    private void OnTriggerEnter2D(Collider2D other)
    {
      if (other.attachedRigidbody.CompareTag("Player"))
      {
        _interpolator.SetEdgeOffset();
      }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
      if (other.attachedRigidbody.CompareTag("Player"))
      {
        _interpolator.SetDefaultOffset();
      }
    }
  }
}