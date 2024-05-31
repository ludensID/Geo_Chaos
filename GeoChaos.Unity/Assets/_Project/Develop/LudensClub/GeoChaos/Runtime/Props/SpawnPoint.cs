using LudensClub.GeoChaos.Runtime.Constants;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Props
{
  [AddComponentMenu(ACC.Names.SPAWN_POINT)]
  public class SpawnPoint : MonoBehaviour
  {
    public EntityType EntityId;

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
      Gizmos.color = EntityId == EntityType.Hero ? Color.green : Color.red;
      Gizmos.DrawSphere(transform.position, 0.5f);
    }
#endif
  }
}