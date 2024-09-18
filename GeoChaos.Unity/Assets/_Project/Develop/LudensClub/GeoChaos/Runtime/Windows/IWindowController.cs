namespace LudensClub.GeoChaos.Runtime.Windows
{
  public interface IWindowController
  {
    WindowType Id { get; }
    bool IsOpened { get; }
      
    void Open();
    void Close();
  }
}