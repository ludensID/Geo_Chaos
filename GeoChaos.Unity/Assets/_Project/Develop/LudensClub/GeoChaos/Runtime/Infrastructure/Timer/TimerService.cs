using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public sealed class TimerService : ITimerService, IFixedTickable
  {
    private readonly List<ITimerable> _timers = new List<ITimerable>();
    
    public void AddTimer(ITimerable elem) => _timers.Add(elem);
    public void RemoveTimer(ITimerable elem) => _timers.Remove(elem);
    
    public void FixedTick()
    {
      foreach (ITimerable timer in _timers.ToArray())
      {
        timer.TimeLeft -= Time.fixedDeltaTime;
        if (timer.TimeLeft <= 0)
          RemoveTimer(timer);
      }
    }
  }
}