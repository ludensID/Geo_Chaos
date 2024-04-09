using System.Collections.Generic;
using UnityEngine;

namespace LudensClub.GeoChaos.Debugging.Monitoring
{
  public class EcsEntityView : MonoBehaviour
  {
    public List<EcsComponentView> Components = new();
  }
}