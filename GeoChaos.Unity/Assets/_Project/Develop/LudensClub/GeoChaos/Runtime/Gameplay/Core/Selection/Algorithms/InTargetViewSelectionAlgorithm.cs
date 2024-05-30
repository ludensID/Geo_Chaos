using LudensClub.GeoChaos.Runtime.Gameplay.Ring;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Selection;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core.Selection
{
  public class InTargetViewSelectionAlgorithm : ISelectionAlgorithm
  {
    private readonly float _viewAngle;

    public InTargetViewSelectionAlgorithm(float viewAngle)
    {
      _viewAngle = viewAngle;
    }
    
    public void Select(EcsEntities origins, EcsEntities selections)
    {
      foreach (EcsEntity origin in origins)
      foreach (EcsEntity selection in selections)
      {
        Transform originTransform = origin.Get<ViewRef>().View.transform;
        Vector3 selectionPosition = selection.Get<ViewRef>().View.transform.position;
        Vector3 selectionVector = selectionPosition - originTransform.position;
        if (Vector3.Angle(originTransform.right, selectionVector) > _viewAngle)
          selection.Del<Selected>();
      }
    }
  }
}