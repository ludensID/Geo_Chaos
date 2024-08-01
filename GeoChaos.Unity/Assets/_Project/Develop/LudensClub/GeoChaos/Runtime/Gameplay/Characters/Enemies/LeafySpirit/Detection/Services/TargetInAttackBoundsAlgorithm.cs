using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.AI;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Selection;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Detection
{
  public class TargetInAttackBoundsAlgorithm : ISelectionAlgorithm
  {
    private readonly LeafySpiritConfig _config;

    public TargetInAttackBoundsAlgorithm(IConfigProvider configProvider)
    {
      _config = configProvider.Get<LeafySpiritConfig>();
    }

    public void Select(EcsEntities origins, EcsEntities marks)
    {
      foreach (EcsEntity origin in origins)
      foreach (EcsEntity selection in marks)
      {
        Vector2 originPosition = origin.Get<ViewRef>().View.transform.position;
        Vector2 selectionPosition = selection.Get<ViewRef>().View.transform.position;
        Vector2 bounds = selection.Get<PatrolBounds>().Bounds;
        var sqrDistance = _config.AttackDistance * _config.AttackDistance;

        bool left = (originPosition - new Vector2(bounds.x, selectionPosition.y)).sqrMagnitude <= sqrDistance;
        bool right = (originPosition - new Vector2(bounds.y, selectionPosition.y)).sqrMagnitude <= sqrDistance;
        if ((originPosition.x < bounds.x || originPosition.y > bounds.y) && !left && !right)
          selection.Del<Marked>();
      }
    }
  }
}