using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Gameplay.FlexibleCamera
{
  [AddComponentMenu(ACC.Names.EDGE_TRIGGER_DETECTOR)]
  public class EdgeTriggerDetector : HeroDetector
  {
    private IEdgeOffsetSetter _setter;

    [Inject]
    public void Construct(IEdgeOffsetSetter setter)
    {
      _setter = setter;
    }

    public override void OnHeroEnter()
    {
        _setter.SetEdgeOffset();
    }

    public override void OnHeroExit()
    {
        _setter.SetDefaultOffset();
    }
  }
}