using System;
using TriInspector;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Spine
{
  [Serializable]
  public class SpineVariable<TValue> : ISpineVariable
  {
    [SerializeField]
    [HideLabel]
    private TValue _value;

    public TValue Value
    {
      get => GetValue<TValue>();
      set => SetValue(value);
    }

    public TIValue GetValue<TIValue>()
    {
      if (_value is not TIValue value)
        throw new ArgumentException();

      return value;
    }

    public void SetValue<TIValue>(TIValue value)
    {
      if (value is not TValue tValue)
        throw new ArgumentException();

      _value = tValue;
    }

    public object GetValue()
    {
      return _value;
    }

    public void SetValue(object value)
    {
      _value = (TValue)value;
    }
  }
}