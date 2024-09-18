namespace LudensClub.GeoChaos.Runtime.Windows
{
  public interface IWindowManager
  {
    IWindowController Current { get; }
    void Add(IWindowController window);
    void Open(WindowType id);
    void OpenAsNew(WindowType id);
    void Close();
    void Close(WindowType id);
  }
}