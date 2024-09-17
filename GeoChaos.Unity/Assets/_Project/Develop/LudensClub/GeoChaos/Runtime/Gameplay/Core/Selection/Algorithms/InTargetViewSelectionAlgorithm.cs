using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Selection;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core.Selection
{
  public class InTargetViewSelectionAlgorithm : ISelectionAlgorithm
  {
    private readonly SelectionData _data;

    public InTargetViewSelectionAlgorithm(SelectionData data)
    {
      _data = data;
    }
    
    public void Select(EcsEntities origins, EcsEntities marks)
    {
      foreach (EcsEntity origin in origins)
      foreach (EcsEntity selection in marks)
      {
        Transform originTransform = origin.Get<ViewRef>().View.transform;
        Vector2 selectionPosition = selection.Get<ViewRef>().View.transform.position;
        Vector2 selectionVector = selectionPosition - (Vector2) originTransform.position;
        if (Vector2.Angle(originTransform.right, selectionVector) > _data.ViewAngle)
          selection.Del<Marked>();
      }
    }
  }
}