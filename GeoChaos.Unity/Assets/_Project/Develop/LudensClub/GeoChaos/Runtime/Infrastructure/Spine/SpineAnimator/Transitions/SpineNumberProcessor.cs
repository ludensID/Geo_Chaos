using System;
using TriInspector;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Spine
{
  [Serializable]
  public class SpineNumberProcessor<TNumber> : ISpineProcessor
  {
    [HideLabel]
    public NumberOperationType Operation;
    [HideLabel]
    public TNumber Number; 
    
    public bool Execute(ISpineVariable variable)
    {
      var value = variable.GetValue<TNumber>();
      float comparable = GetNumber(value);
      float number = GetNumber(Number);

      return Operation switch
      {
        NumberOperationType.Equal => comparable == number,
        NumberOperationType.NotEqual => comparable != number,
        NumberOperationType.LessThan => comparable < number,
        NumberOperationType.MoreThan => comparable > number,
        NumberOperationType.LessThanOrEqual => comparable <= number,
        NumberOperationType.MoreThanOrEqual => comparable >= number,
        _ => throw new ArgumentOutOfRangeException()
      };
    }

    private static float GetNumber(TNumber value)
    {
      float comparable = float.NaN;
      comparable = value switch
      {
        int intValue => intValue,
        float floatValue => floatValue,
        _ => comparable
      };

      if (float.IsNaN(comparable))
        throw new ArgumentException();
      return comparable;
    }
  }
}