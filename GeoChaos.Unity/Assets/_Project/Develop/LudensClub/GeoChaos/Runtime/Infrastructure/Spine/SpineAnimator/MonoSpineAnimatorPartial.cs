﻿#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using TriInspector;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Spine
{
  public partial class MonoSpineAnimator<TParameterEnum, TAnimationEnum>
  {
    private readonly Dictionary<TParameterEnum, object> _parameters = new Dictionary<TParameterEnum, object>();

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
      foreach (KeyValuePair<TParameterEnum, object> pair in _parameters)
      {
        ISpineVariable variable = _showParameters.Find(x => x.Id.Equals(pair.Key)).Variable;
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
          _parameters[tuple.Id] = tuple.Variable.GetValue();

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
      public TParameterEnum Id;

      [SerializeReference]
      [HideReferencePicker]
      [InlineProperty]
      [HideLabel]
      public ISpineVariable Variable;

      public VariableTuple(TParameterEnum id, ISpineVariable variable)
      {
        Id = id;
        Variable = variable;
      }
    }
  }
}
#endif