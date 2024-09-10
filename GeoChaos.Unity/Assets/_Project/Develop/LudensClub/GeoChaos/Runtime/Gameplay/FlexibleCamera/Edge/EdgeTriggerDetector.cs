using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Gameplay.FlexibleCamera
{
  [AddComponentMenu(ACC.Names.EDGE_TRIGGER_DETECTOR)]
  public class EdgeTriggerDetector : MonoBehaviour
  {
    private IEdgeOffsetSetter _setter;

    [Inject]
    public void Construct(IEdgeOffsetSetter setter)
    {
      _setter = setter;
    }
      
    private void OnTriggerEnter2D(Collider2D other)
    {
      if (other.attachedRigidbody.CompareTag("Player"))
      {
        _setter.SetEdgeOffset();
      }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
      if (other.attachedRigidbody.CompareTag("Player"))
      {
        _setter.SetDefaultOffset();
      }
    }
  }
}