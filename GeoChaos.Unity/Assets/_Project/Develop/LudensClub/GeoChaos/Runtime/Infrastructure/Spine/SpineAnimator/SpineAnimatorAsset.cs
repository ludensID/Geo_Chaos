using System;
using System.Collections.Generic;
using TriInspector;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Spine
{
  public abstract class SpineAnimatorAsset<TParameterEnum, TAnimationEnum> : ScriptableObject
    where TParameterEnum : Enum where TAnimationEnum : Enum
  {
    [HideReferencePicker]
    [ListDrawerSettings(AlwaysExpanded = true)]
    [ValidateInput("ValidateParameters")]
    public List<SpineParameter<TParameterEnum>> Parameters;

    [ListDrawerSettings(ShowElementLabels = true)]
    public List<SpineLayer<TAnimationEnum>> Layers;

    [ListDrawerSettings(ShowElementLabels = true)]
    public List<SpineTransition<TParameterEnum, TAnimationEnum>> Transitions;

#if UNITY_EDITOR
    private TriValidationResult ValidateParameters()
    {
      int listCount = Parameters.Count;
      int enumCount = Enum.GetValues(typeof(TParameterEnum)).Length;
      return listCount != enumCount
        ? TriValidationResult.Warning(
          $"Count of parameters ({listCount}) is not equal count of parameter types ({enumCount})")
        : TriValidationResult.Valid;
    }
#endif
  }
}