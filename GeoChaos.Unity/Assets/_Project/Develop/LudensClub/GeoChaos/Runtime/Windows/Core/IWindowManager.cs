using System.Collections.Generic;

namespace LudensClub.GeoChaos.Runtime.Windows
{
  public interface IWindowManager
  {
    IWindowController Current { get; }
    List<IWindowController> Windows { get; }
    
    void Add(IWindowController window);
    void Open(WindowType id);
    void OpenAsNew(WindowType id);
    void Close();
    void Close(WindowType id);
    void CloseAll();
      
    IWindowController FindWindowById(WindowType id);
  }
}