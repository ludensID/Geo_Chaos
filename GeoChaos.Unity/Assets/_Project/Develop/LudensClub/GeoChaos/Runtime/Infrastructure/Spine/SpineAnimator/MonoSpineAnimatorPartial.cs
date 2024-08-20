#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using TriInspector;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Spine
{
  public partial class MonoSpineAnimator
  {
    private readonly Dictionary<string, object> _parameters = new Dictionary<string, object>();

    [SerializeField]
    [ListDrawerSettings(Draggable = false, HideRemoveButton = true, HideAddButton = true)]
    [PropertyOrder(2)]
    [LabelText("Parameters")]
    [EnableInPlayMode]
    private List<VariableTuple> _showParameters = new List<VariableTuple>();

    private bool _dirty;

    [Button("Recreate Animator")]
    [PropertyOrder(0)]
    [EnableInPlayMode]
    private void RecreateAnimator()
    {
      CreateAnimator();
    }

    private void CheckUserParameters()
    {
      foreach (KeyValuePair<string, object> pair in _parameters)
      {
        ISpineVariable variable = _showParameters.Find(x => x.ParameterName == pair.Key).Variable;
        object value = variable.GetValue();
        if (!value.Equals(pair.Value))
        {
          _dirty = true;
          _needCheck = true;
        }
      }
    }

    private void SyncUserParameters()
    {
      if (_dirty)
      {
        foreach (VariableTuple tuple in _showParameters)
          _parameters[tuple.ParameterName] = tuple.Variable.GetValue();

        _dirty = false;
      }
    }

    [Serializable]
    [DeclareHorizontalGroup(nameof(VariableTuple))]
    public class VariableTuple
    {
      [GroupNext(nameof(VariableTuple))]
      [HideLabel]
      [DisplayAsString]
      public string ParameterName;

      [SerializeReference]
      [HideReferencePicker]
      [InlineProperty]
      [HideLabel]
      public ISpineVariable Variable;

      public VariableTuple(string parameterName, ISpineVariable variable)
      {
        ParameterName = parameterName;
        Variable = variable;
      }
    }
  }
}
#endif