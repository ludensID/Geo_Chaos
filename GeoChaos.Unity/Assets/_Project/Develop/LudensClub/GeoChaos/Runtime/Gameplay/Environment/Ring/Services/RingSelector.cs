using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core.Selection;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Selection;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Ring
{
  public class RingSelector : EcsEntitySelector
  {
    private readonly SelectionData _data = new SelectionData();
    private readonly HeroConfig _config;

    public RingSelector(ISelectionAlgorithmFactory factory, IConfigProvider configProvider)
    {
      _config = configProvider.Get<HeroConfig>();
      _algorithms.AddRange(new ISelectionAlgorithm[]
      {
        factory.Create<InRadiusSelectionAlgorithm>(_data),
        factory.Create<ReachedRingAlgorithm>(),
        factory.Create<InTargetViewSelectionAlgorithm>(_data),
        factory.Create<NearestTargetSelectionAlgorithm>()
      });

      Update(); 
    }

    public override void Select<TComponent>(EcsEntities origins, EcsEntities targets, EcsEntities marks)
    {
      Update();
      base.Select<TComponent>(origins, targets, marks);
    }

    private void Update()
    {
      _data.Radius = _config.HookRadius;
      _data.ViewAngle = _config.RingViewAngle;
    }
  }
}