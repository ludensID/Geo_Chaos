using System.Collections.Generic;
using LudensClub.GeoChaos.Editor.Monitoring.World;
using UnityEngine;

namespace LudensClub.GeoChaos.Editor.Monitoring.Universe
{
  [AddComponentMenu(ACC.Names.ECS_UNIVERSE_VIEW)]
  public class EcsUniverseView : MonoBehaviour
  {
    public List<EcsWorldView> Worlds = new List<EcsWorldView>();
  }
}