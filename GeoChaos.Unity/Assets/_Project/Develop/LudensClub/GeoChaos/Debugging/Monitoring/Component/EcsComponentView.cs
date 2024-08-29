using System;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using TriInspector;
using UnityEditor;
using UnityEngine;

namespace LudensClub.GeoChaos.Debugging.Monitoring
{
  [Serializable]
  public class EcsComponentView
  {
    [LabelText("$" + nameof(ValueName))]
    [SerializeReference]
    [OnValueChanged(nameof(OnValueChanged))]
    public IEcsComponent Value;

    [HideInInspector]
    public string Name;

    [HideInInspector]
    public IEcsEntityPresenter Presenter;

    private string ValueName => ObjectNames.NicifyVariableName(Value?.GetType().Name ?? "[None]");

    private void OnValueChanged()
    {
      Presenter.ChangeComponent(Value);
    }
  }
}