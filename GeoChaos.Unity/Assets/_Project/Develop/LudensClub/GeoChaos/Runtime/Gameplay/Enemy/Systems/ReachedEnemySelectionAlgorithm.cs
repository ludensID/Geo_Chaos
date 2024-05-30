using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Ring;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Selection;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemy
{
  public class ReachedEnemySelectionAlgorithm : ISelectionAlgorithm
  {
    private readonly PhysicsConfig _physics;

    public ReachedEnemySelectionAlgorithm(IConfigProvider configProvider)
    {
      _physics = configProvider.Get<PhysicsConfig>();
    }

    public void Select(EcsEntities origins, EcsEntities selections)
    {
      foreach (EcsEntity origin in origins)
      foreach (EcsEntity selection in selections)
      {
        Vector3 originPosition = origin.Get<ViewRef>().View.transform.position;
        Vector3 selectionPosition = selection.Get<ViewRef>().View.transform.position;

        Vector3 vector = selectionPosition - originPosition;
        RaycastHit2D centerRaycast = Physics2D.Raycast(originPosition, vector.normalized, vector.magnitude,
          _physics.GroundMask);

        if (centerRaycast.collider != null)
          selection.Del<Selected>();
      }
    }
  }
}