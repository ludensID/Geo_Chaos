using System.Collections.Generic;
using UnityEngine;

namespace LudensClub.GeoChaos.Debugging.Monitoring
{
  [AddComponentMenu(ACC.Names.ECS_WORLD_VIEW)]
  public class EcsWorldView : MonoBehaviour
  {
    public List<EcsEntityView> Entities = new List<EcsEntityView>();
  }
}