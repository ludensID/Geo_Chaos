using System;
using System.Collections.Generic;
using LudensClub.GeoChaos.Runtime.Utils;
using TriInspector;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Spine
{
  [Serializable]
  public class SpineNumberProcessor<TNumber> : ISpineProcessor
  {
    private static List<TriDropdownItem<NumberOperationType>> _operationStrings
      = new List<TriDropdownItem<NumberOperationType>>
      {
        new TriDropdownItem<NumberOperationType> { Text = "=", Value = NumberOperationType.Equal },
        new TriDropdownItem<NumberOperationType> { Text = "!=", Value = NumberOperationType.NotEqual },
        new TriDropdownItem<NumberOperationType> { Text = "<", Value = NumberOperationType.LessThan },
        new TriDropdownItem<NumberOperationType> { Text = ">", Value = NumberOperationType.MoreThan },
        new TriDropdownItem<NumberOperationType> { Text = "<=", Value = NumberOperationType.LessThanOrEqual },
        new TriDropdownItem<NumberOperationType> { Text = ">=", Value = NumberOperationType.MoreThanOrEqual },
      };

    [HideLabel]
    [Dropdown("$" + nameof(_operationStrings))]
    public NumberOperationType OperationString;

    [HideLabel]
    public TNumber Number;

    public bool Execute(ISpineVariable variable)
    {
      var value = variable.GetValue<TNumber>();
      float comparing = GetFloat(value);
      float number = GetFloat(Number);

      return OperationString switch
      {
        NumberOperationType.Equal => comparing.ApproximatelyEqual(number),
        NumberOperationType.NotEqual => !comparing.ApproximatelyEqual(number),
        NumberOperationType.LessThan => comparing < number,
        NumberOperationType.MoreThan => comparing > number,
        NumberOperationType.LessThanOrEqual => comparing <= number,
        NumberOperationType.MoreThanOrEqual => comparing >= number,
        _ => throw new ArgumentOutOfRangeException()
      };
    }

    private static float GetFloat(TNumber value)
    {
      return value switch
      {
        int intValue => intValue,
        float floatValue => floatValue,
        _ => throw new ArgumentException()
      };
    }
  }
}