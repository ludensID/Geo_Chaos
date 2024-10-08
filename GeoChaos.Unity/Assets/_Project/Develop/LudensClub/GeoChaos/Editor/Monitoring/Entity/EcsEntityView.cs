using System.Collections.Generic;
using System.Linq;
using LudensClub.GeoChaos.Editor.Monitoring.Component;
using TriInspector;
using UnityEngine;

namespace LudensClub.GeoChaos.Editor.Monitoring.Entity
{
  [AddComponentMenu(ACC.Names.ECS_ENTITY_VIEW)]
  public class EcsEntityView : MonoBehaviour
  {
    public List<EcsComponentView> ComponentPull = new List<EcsComponentView>();

    [SerializeReference]
    [HideReferencePicker]
    public List<IEcsComponentView> Components = new List<IEcsComponentView>();

    private IEcsEntityPresenter _presenter;

    private bool AnyComponents => ComponentPull.Any();

    public void SetController(IEcsEntityPresenter presenter)
    {
      _presenter = presenter;
    }

    [EnableIf(nameof(AnyComponents))]
    [GUIColor(0.8f, 1.0f, 0.6f)]
    [PropertyOrder(1)]
    [Button("Add Components")]
    public void AddComponents()
    {
      _presenter.AddComponents();
    }

    [EnableIf(nameof(AnyComponents))]
    [GUIColor(1.0f, 0.6f, 0.6f)]
    [PropertyOrder(1)]
    [Button("Remove Components")]
    public void RemoveComponents()
    {
      _presenter.RemoveComponents();
    }

    [GUIColor(0.6f, 0.9f, 1.0f)]
    [PropertyOrder(1)]
    [Button("Clear Pull")]
    public void Clear()
    {
      ComponentPull.Clear();
    }
  }
}