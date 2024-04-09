using System.Collections.Generic;
using UnityEngine;

namespace LudensClub.GeoChaos.Debugging.Monitoring
{
  public class EcsWorldView : MonoBehaviour
  {
    public List<EcsEntityView> Entities = new();
  }
}