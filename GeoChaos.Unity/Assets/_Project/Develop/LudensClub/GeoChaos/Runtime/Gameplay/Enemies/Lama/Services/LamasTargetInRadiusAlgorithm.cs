using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Selection;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemies.Lama
{
  public class LamasTargetInRadiusAlgorithm : ISelectionAlgorithm
  {
    private readonly LamaConfig _config;

    public LamasTargetInRadiusAlgorithm(IConfigProvider configProvider)
    {
      _config = configProvider.Get<LamaConfig>();
    }
    
    public void Select(EcsEntities origins, EcsEntities marks)
    {
      foreach (EcsEntity origin in origins)
      foreach (EcsEntity selection in marks)
      {
        Transform targetTransform = origin.Get<ViewRef>().View.transform;
        Transform selectionTransform = selection.Get<ViewRef>().View.transform;
        if (Vector2.Distance(targetTransform.position, selectionTransform.position) > _config.ViewRadius)
          selection.Del<Marked>();
      }
    }
  }
}