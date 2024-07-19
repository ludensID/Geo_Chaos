using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public sealed class TimerService : ITimerService, IFixedTickable
  {
    private readonly List<TimerableUnit> _timers = new List<TimerableUnit>();
    private readonly List<ITimerable> _toRemoveTimers = new List<ITimerable>();

    public void AddTimer(ITimerable elem, bool unscaled = false)
    {
      _timers.Add(new TimerableUnit(elem, unscaled));
    }

    public void RemoveTimer(ITimerable elem)
    {
      TimerableUnit unit = _timers.Find(x => x.Timer == elem);
      if (unit.Timer != null)
        _timers.Remove(unit);
    }

    public void FixedTick()
    {
      _toRemoveTimers.Clear();
      foreach (TimerableUnit unit in _timers)
      {
        unit.Timer.TimeLeft -= unit.Unscaled ? Time.fixedUnscaledDeltaTime : Time.fixedDeltaTime;

        if (unit.Timer.TimeLeft <= 0)
          _toRemoveTimers.Add(unit.Timer);
      }
      
      foreach (ITimerable timer in _toRemoveTimers)
      {
        RemoveTimer(timer);
      }
    }

    public struct TimerableUnit
    {
      public ITimerable Timer;
      public bool Unscaled;

      public TimerableUnit(ITimerable timer, bool unscaled)
      {
        Timer = timer;
        Unscaled = unscaled;
      }
    }
  }
}