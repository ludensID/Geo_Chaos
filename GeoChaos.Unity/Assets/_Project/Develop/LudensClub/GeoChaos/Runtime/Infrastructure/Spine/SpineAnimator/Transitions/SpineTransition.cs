using System;
using System.Collections.Generic;
using Spine.Unity;
using TriInspector;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Spine
{
  [Serializable]
  public class SpineTransition
  {
    [SerializeField]
    [SpineAnimation]
    private List<string> _origins = new List<string>();

    [SerializeField]
    [SpineAnimation]
    [ValidateInput(TriConstants.VALIDATE + nameof(Destination))]
    private string _destination;

    [SerializeField]
    [OnValueChanged(TriConstants.ON + nameof(Conditions) + TriConstants.CHANGED)]
    [ValidateInput(TriConstants.VALIDATE + nameof(Conditions))]
    private List<SpineCondition> _conditions = new List<SpineCondition>();

    [SerializeField]
    private bool _isHold;

    public List<string> Origins => _origins;
    public string Destination => _destination;
    public List<SpineCondition> Conditions => _conditions;
    public bool IsHold => _isHold;

#if UNITY_EDITOR
    [Button("Clear")]
    [PropertyOrder(0)]
    [GUIColor(1f,0f,0f)]
    private void Clear()
    {
      _origins.Clear();
      _destination = "";
      _conditions.Clear();
      _isHold = false;
    }
      
    private TriValidationResult ValidateDestination()
    {
      if (_origins.Contains(_destination))
      {
        return TriValidationResult.Error("Animation can not transit into itself. Change destination animation or remove this animation from origin animations");
      }

      return TriValidationResult.Valid;
    }
    
    private void OnConditionsChanged()
    {
      for (var i = 0; i < Conditions.Count; i++)
      {
        for (int j = i + 1; j < Conditions.Count; j++)
        {
          if (Conditions[i].Parameter == Conditions[j].Parameter
            && Conditions[i].Processor == Conditions[j].Processor)
          {
            Conditions[j].WasChanged = true;
          }
        }
      }
    }

    private TriValidationResult ValidateConditions()
    {
      foreach (SpineCondition condition in Conditions)
      {
        if (Conditions.Exists(x => condition != x && condition.Parameter == x.Parameter))
          return TriValidationResult.Error($"Condition with {condition.Parameter} parameter already exists");
      }

      return TriValidationResult.Valid;
    }
#endif
  }
}