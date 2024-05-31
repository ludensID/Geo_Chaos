using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core.Selection;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Selection;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemy
{
  public class DamagableEntitySelector : EcsEntitySelector
  {
    private readonly SelectionData _data = new SelectionData();
    private readonly HeroConfig _config;

    public DamagableEntitySelector(ISelectionAlgorithmFactory factory, IConfigProvider configProvider)
    {
      _config = configProvider.Get<HeroConfig>();
      _algorithms.AddRange(new ISelectionAlgorithm[]
      {
        factory.Create<InRadiusSelectionAlgorithm>(_data),
        factory.Create<ReachedEnemySelectionAlgorithm>(),
        factory.Create<InTargetViewSelectionAlgorithm>(_data),
        factory.Create<NearestTargetSelectionAlgorithm>()
      });

      Update();
    }

    public override void Select(EcsEntities origins, EcsEntities targets, EcsEntities selections)
    {
      Update();
      base.Select(origins, targets, selections);
    }

    private void Update()
    {
      _data.Radius = _config.AutoShootRadius;
      _data.ViewAngle = _config.EnemyViewAngle;
    }
  }
}