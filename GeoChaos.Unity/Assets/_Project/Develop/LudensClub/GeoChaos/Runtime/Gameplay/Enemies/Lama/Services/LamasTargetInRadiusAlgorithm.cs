using LudensClub.GeoChaos.Runtime.AI;
using LudensClub.GeoChaos.Runtime.Gameplay.AI;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Selection;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemies.Lama
{
  public class LamasTargetInRadiusAlgorithm : ISelectionAlgorithm
  {
    public void Select(EcsEntities origins, EcsEntities marks)
    {
      foreach (EcsEntity origin in origins)
      foreach (EcsEntity selection in marks)
      {
        Transform targetTransform = origin.Get<ViewRef>().View.transform;
        Transform selectionTransform = selection.Get<ViewRef>().View.transform;
        var ctx = selection.Get<BrainContext>().Cast<LamaContext>();
        if (Vector2.Distance(targetTransform.position, selectionTransform.position) > ctx.ViewRadius)
          selection.Del<Marked>();
      }
    }
  }
}