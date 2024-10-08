using System.Collections.Generic;
using Zenject;

namespace LudensClub.GeoChaos.Editor.Watchers
{
  public class GlobalWatcher : ITickable
  {
    private readonly List<IWatcher> _watchers;

    public GlobalWatcher(List<IWatcher> watchers)
    {
      _watchers = watchers;
    }

    public void Tick()
    {
      foreach (IWatcher watcher in _watchers)
      {
        if (watcher.IsDifferent())
        {
          watcher.Assign();
          watcher.OnChanged();
        }
      }
    }
  }
}