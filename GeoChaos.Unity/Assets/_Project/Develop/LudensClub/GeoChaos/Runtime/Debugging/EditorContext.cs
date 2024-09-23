#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using Zenject;

namespace LudensClub.GeoChaos.Runtime
{
  public class EditorContext
  {
    private readonly List<Action> _listeners = new List<Action>();
    
    public DiContainer Container { get; } = new DiContainer();

    public EditorContext()
    {
      Container.BindInstance(this);
    }
    
    public void AddListener(Action action)
    {
      _listeners.Add(action);
    }

    public void RemoveListener(Action action)
    {
      _listeners.Remove(action);
    }

    public void ResolveRoots()
    {
      Container.ResolveRoots();
      foreach (Action listener in new List<Action>(_listeners))
      {
        listener?.Invoke();
      }
    }
  }
}
#endif