using System;
using TriInspector;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Spine
{
  [Serializable]
  [DeclareHorizontalGroup(nameof(SpineCondition<TParameterEnum>))]
  public class SpineCondition<TParameterEnum> : ISpineCondition where TParameterEnum : Enum
  {
    [GroupNext(nameof( SpineCondition<TParameterEnum>))]
    [HideLabel]
    public TParameterEnum Parameter;

    [SerializeReference]
    [HideReferencePicker]
    [InlineProperty]
    [HideLabel]
    public ISpineProcessor Processor;

    public ISpineVariable Variable { get; set; }

    public bool Execute()
    {
      return Processor.Execute(Variable);
    }

    public TIParameterEnum GetParameterId<TIParameterEnum>() where TIParameterEnum : Enum
    {
      if (Parameter is not TIParameterEnum parameter)
        throw new ArgumentException();

      return parameter;
    }
  }
}