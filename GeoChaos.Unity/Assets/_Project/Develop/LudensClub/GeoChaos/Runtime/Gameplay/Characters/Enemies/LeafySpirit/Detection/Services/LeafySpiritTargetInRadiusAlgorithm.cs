using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Selection;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Detection
{
  public class LeafySpiritTargetInRadiusAlgorithm : ISelectionAlgorithm
  {
    private readonly LeafySpiritConfig _config;

    public LeafySpiritTargetInRadiusAlgorithm(IConfigProvider configProvider)
    {
     _config = configProvider.Get<LeafySpiritConfig>();
    }
    
    public void Select(EcsEntities origins, EcsEntities marks)
    {
      foreach (EcsEntity hero in origins)
      foreach (EcsEntity spirit in marks)
      {
        Vector3 heroPosition = hero.Get<ViewRef>().View.transform.position;
        Vector3 spiritPosition = spirit.Get<ViewRef>().View.transform.position;
        if (Mathf.Abs(heroPosition.y - spiritPosition.y) > _config.MaxVerticalDistance)
          spirit.Del<Marked>();
      }
    }
  }
}