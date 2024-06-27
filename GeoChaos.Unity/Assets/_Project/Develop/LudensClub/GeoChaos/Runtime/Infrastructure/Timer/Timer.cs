using System;
using TriInspector;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  [Serializable]
  [InlineProperty]
  public class Timer : ITimerable, IComparable, IComparable<Timer>
  {
    public static implicit operator float(Timer obj)
    {
      return obj.TimeLeft;
    }

    public static implicit operator Timer(float obj)
    {
      return new Timer() { TimeLeft = obj };
    }

    [SerializeField]
    [HideLabel]
    private float _timeLeft;

    public float TimeLeft
    {
      get => _timeLeft;
      set => _timeLeft = value;
    }

    public int CompareTo(object obj)
    {
      if (obj != null && obj is not Timer)
        throw new ArgumentException($"Object is not a {nameof(Timer)}");

      return CompareTo((Timer)obj);
    }

    public int CompareTo(Timer other)
    {
      return other != null
        ? _timeLeft.CompareTo(other._timeLeft)
        : 1;
    }
  }
}