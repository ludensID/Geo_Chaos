using System;
using System.Collections.Generic;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  [Serializable]
  public class CallbackProperty<TValue> : IEquatable<CallbackProperty<TValue>>
  {
    [SerializeField]
    private TValue _value;

    public TValue Value
    {
      get => _value;
      set
      {
        if (_value?.Equals(value) ?? value?.Equals(_value) ?? true)
          return;

        _value = value;
        OnChanged?.Invoke();
      }
    }

    public delegate void ChangedHandler();

    public event ChangedHandler OnChanged;

    public CallbackProperty()
    {
    }

    public CallbackProperty(TValue value)
    {
      _value = value;
    }

    public void SetValueSilent(TValue value)
    {
      _value = value;
    }

    public bool Equals(CallbackProperty<TValue> other)
    {
      if (ReferenceEquals(null, other))
        return false;

      if (ReferenceEquals(this, other))
        return true;

      return EqualityComparer<TValue>.Default.Equals(_value, other._value);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj))
        return false;

      if (ReferenceEquals(this, obj))
        return true;

      if (obj.GetType() != GetType())
        return false;

      return Equals((CallbackProperty<TValue>)obj);
    }

    public override int GetHashCode() => EqualityComparer<TValue>.Default.GetHashCode(_value);

    public static bool operator ==(CallbackProperty<TValue> left, CallbackProperty<TValue> right)
    {
      if (ReferenceEquals(left, right))
        return true;

      if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
        return false;

      return left.Equals(right);
    }

    public static bool operator !=(CallbackProperty<TValue> left, CallbackProperty<TValue> right)
    {
      return !(left == right);
    }

    public static implicit operator TValue(CallbackProperty<TValue> obj)
    {
      return obj.Value;
    }

    public static explicit operator CallbackProperty<TValue>(TValue obj)
    {
      return new CallbackProperty<TValue>(obj);
    }
  }
}