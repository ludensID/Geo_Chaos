using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Selection;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Detection
{
  public class TargetInRadiusAlgorithm : ISelectionAlgorithm
  {
    public void Select(EcsEntities origins, EcsEntities marks)
    {
      foreach (EcsEntity origin in origins)
      foreach (EcsEntity selection in marks)
      {
        float originPoint = origin.Get<ViewRef>().View.transform.position.x;
        float selectionPoint = selection.Get<ViewRef>().View.transform.position.x;
        float radius = selection.Get<ViewRadius>().Radius;

        if (Mathf.Abs(originPoint - selectionPoint) > radius)
          selection.Del<Marked>();
      }
    }
  }
}