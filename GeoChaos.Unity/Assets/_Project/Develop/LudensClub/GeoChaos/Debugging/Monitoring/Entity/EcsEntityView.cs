using System.Collections.Generic;
using System.Linq;
using TriInspector;
using UnityEngine;

namespace LudensClub.GeoChaos.Debugging.Monitoring
{
  [AddComponentMenu(ACC.Names.ECS_ENTITY_VIEW)]
  public class EcsEntityView : MonoBehaviour
  {
    public List<EcsComponentView> Components = new List<EcsComponentView>();
    public List<EcsComponentView> ComponentPull = new List<EcsComponentView>();
    
    private IEcsEntityPresenter _presenter;

    private bool AnyComponents => ComponentPull.Any();

    public void SetController(IEcsEntityPresenter presenter)
    {
      _presenter = presenter;
    }

    [EnableIf(nameof(AnyComponents))]
    [GUIColor(0.8f, 1.0f, 0.6f)]
    [Button("Add Components")]
    public void AddComponents()
    {
      _presenter.AddComponents();
    }

    [EnableIf(nameof(AnyComponents))]
    [GUIColor(1.0f, 0.6f, 0.6f)]
    [Button("Remove Components")]
    public void RemoveComponents()
    {
      _presenter.RemoveComponents();
    }

    [GUIColor(0.6f, 0.9f, 1.0f)]
    [Button("Clear Pull")]
    public void Clear()
    {
      ComponentPull.Clear();
    }
  }
}