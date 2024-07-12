using System;
using TriInspector;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Spine
{
  [Serializable]
  [DeclareHorizontalGroup(nameof(SpineParameter<TParameterEnum>))]
  public class SpineParameter<TParameterEnum> : ISpineParameter where TParameterEnum : Enum
  {
    [GroupNext(nameof(SpineParameter<TParameterEnum>))]
    [SerializeField]
    [HideLabel]
    private TParameterEnum _id;

    [SerializeField]
    [HideLabel]
    private SpineVariableType _variableType;

    [SerializeReference]
    [HideInInspector]
    private ISpineVariable _variable;

    [SerializeField]
    [HideInInspector]
    private bool _isTrigger;

    public TParameterEnum Id => _id;

    public ISpineVariable Variable => _variable;

    public bool IsTrigger => _isTrigger;
  }
}