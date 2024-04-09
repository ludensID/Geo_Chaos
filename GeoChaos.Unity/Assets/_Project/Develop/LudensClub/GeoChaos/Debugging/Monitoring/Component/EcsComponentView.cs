using System;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using TriInspector;
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

    private string ValueName => Value != null ? Value.GetType().Name : "[None]";
  }
}
