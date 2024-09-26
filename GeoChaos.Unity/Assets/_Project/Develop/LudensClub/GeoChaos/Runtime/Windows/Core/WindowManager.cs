using System;
using System.Collections.Generic;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Windows
{
  public class WindowManager : IWindowManager
  {
    private readonly List<IWindowController> _windows = new List<IWindowController>();
    private readonly List<IWindowController> _stack = new List<IWindowController>();
    private readonly SpecifiedClosure<IWindowController, WindowType> _hasWindowIdClosure;

    public IWindowController Current => _stack.Count > 0 ? _stack[^1] : null;

    public WindowManager()
    {
      _hasWindowIdClosure = new SpecifiedClosure<IWindowController, WindowType>((ctrl, id) => ctrl.Id == id);
    }

    public void Add(IWindowController window)
    {
      _windows.Add(window);
    }

    public IWindowController FindWindowById(WindowType id)
    {
      return _windows.Find(_hasWindowIdClosure.SpecifyPredicate(id));
    }

    public void Open(WindowType id)
    {
      OpenWindowInternal(id);
    }

    public void OpenAsNew(WindowType id)
    {
      IWindowController window = FindWindowById(id);
      Current?.Close();
      _stack.Clear();
      window.Open();
      _stack.Add(window);
    }

    public void CloseAll()
    {
      while (Current != null)
        Close();
    }

    public void Close()
    {
      if (Current != null)
      {
        Current.Close();
        _stack.Remove(Current);
        Current?.Open();
      }
    }

    public void Close(WindowType id)
    {
      if (Current != null)
      {
        if(Current.Id == id)
          Close();
        else
          _stack.Remove(FindWindowById(id));
      }
    }

    private void OpenWindowInternal(WindowType id, bool addToStack = true)
    {
      IWindowController window = FindWindowById(id);

      Current?.Close();
      window.Open();

      if (addToStack)
        _stack.Add(window);
    }
  }
}