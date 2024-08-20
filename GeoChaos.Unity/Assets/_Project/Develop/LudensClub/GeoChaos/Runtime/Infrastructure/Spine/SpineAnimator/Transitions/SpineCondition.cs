using System;
using TriInspector;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Spine
{
  [Serializable]
  [DeclareHorizontalGroup(nameof(SpineCondition))]
  public class SpineCondition
  {
    [GroupNext(nameof(SpineCondition))]
    [SerializeField]
    [HideLabel]
    [SpineParameter]
    [OnValueChanged(TriConstants.ON + nameof(Parameter) + TriConstants.CHANGED)]
    private string _parameter;

    [SerializeReference]
    [HideReferencePicker]
    [InlineProperty]
    [HideLabel]
    private ISpineProcessor _processor;

    public ISpineVariable Variable { get; set; }

    public string Parameter
    {
      get => _parameter;
      set => _parameter = value;
    }

    public ISpineProcessor Processor
    {
      get => _processor;
      set => _processor = value;
    }

    public bool Execute()
    {
      return Processor.Execute(Variable);
    }

#if UNITY_EDITOR
    [NonSerialized]
    public bool WasChanged;
    
    private void OnParameterChanged()
    {
      WasChanged = true;
    }
#endif
  }
}