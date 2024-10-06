using System;

namespace LudensClub.GeoChaos.Runtime.Windows
{
  public interface IWindowController
  {
    WindowType Id { get; }
    bool IsOpened { get; }
    WindowModel Model { get; }
    
    event Action OnBeforeOpen;
    event Action OnBeforeClose;
    event Action OnClosed;

    void SetView(WindowView view);
    
    void Open();
    void Close();
  }
}