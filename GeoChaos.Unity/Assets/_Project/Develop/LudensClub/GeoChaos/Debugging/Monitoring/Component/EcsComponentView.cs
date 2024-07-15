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
    [ShowInInspector]
    [SerializeReference]
    public IEcsComponent Value;

    [HideInInspector]
    public string Name;

    private string ValueName => ObjectNames.NicifyVariableName(Value?.GetType().Name ?? "[None]");
  }
}