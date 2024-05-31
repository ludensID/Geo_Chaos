using System.Collections.Generic;
using UnityEngine;

namespace LudensClub.GeoChaos.Debugging.Monitoring
{
  [AddComponentMenu(ACC.Names.ECS_UNIVERSE_VIEW)]
  public class EcsUniverseView : MonoBehaviour
  {
    public List<EcsWorldView> Worlds = new List<EcsWorldView>();
  }
}