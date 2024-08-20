using System;
using TriInspector;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Spine
{
  [Serializable]
  [DeclareHorizontalGroup(nameof(SpineParameter))]
  public class SpineParameter
  {
    [GroupNext(nameof(SpineParameter))]
    [SerializeField]
    [HideLabel]
    private string _name;

    [SerializeField]
    [HideLabel]
    private SpineVariableType _variableType;

    private ISpineVariable _variable;

    public string Name => _name;
    public SpineVariableType VariableType => _variableType;
    public ISpineVariable Variable => _variable;

    public bool IsTrigger => _variableType == SpineVariableType.Trigger;

    public SpineParameter()
    {
      _variable = GetVariable(_variableType);
    }

    private static ISpineVariable GetVariable(SpineVariableType type)
    {
      return type switch
      {
        SpineVariableType.Trigger => new SpineVariable<bool>(),
        SpineVariableType.Bool => new SpineVariable<bool>(),
        SpineVariableType.Integer => new SpineVariable<int>(),
        SpineVariableType.Float => new SpineVariable<float>(),
        _ => throw new ArgumentOutOfRangeException()
      };
    }
  }
}