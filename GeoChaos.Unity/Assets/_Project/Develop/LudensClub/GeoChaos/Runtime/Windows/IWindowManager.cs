namespace LudensClub.GeoChaos.Runtime.Windows
{
  public interface IWindowManager
  {
    IWindowController Current { get; }
    void AddWindow(IWindowController window);
    void OpenWindow(WindowType id);
    void OpenWindowAsNew(WindowType id);
    void CloseWindow();
    void CloseWindow(WindowType id);
  }
}