using System;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  [Serializable]
  public struct OverriddenValue<TValue>
  {
    [SerializeField]
    private TValue _value;

    public bool IsOverridden;

    public TValue Value
    {
      get => _value;
      set
      {
        if (_value?.Equals(value) ?? value?.Equals(_value) ?? true)
          return;

        _value = value;
        IsOverridden = true;
      }
    }

    public OverriddenValue(TValue value = default(TValue))
    {
      _value = value;
      IsOverridden = false;
    }

    public void SetSilent(TValue value)
    {
      _value = value;
    }

    public TValue Flush()
    {
      IsOverridden = false;
      return _value;
    }

    public bool Uncheck()
    {
      bool overridden = IsOverridden;
      IsOverridden = false;
      return overridden;      
    }

    public static implicit operator TValue(OverriddenValue<TValue> obj)
    {
      return obj.Value;
    }

    public static explicit operator OverriddenValue<TValue>(TValue obj)
    {
      return new OverriddenValue<TValue>(obj);
    }
  }
}