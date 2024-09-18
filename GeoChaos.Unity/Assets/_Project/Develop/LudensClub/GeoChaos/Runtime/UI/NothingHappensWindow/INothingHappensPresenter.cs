using LudensClub.GeoChaos.Runtime.Windows;

namespace LudensClub.GeoChaos.Runtime.UI.NothingHappensWindow
{
  public interface INothingHappensPresenter : IWindowController
  {
    void SetView(NothingHappensView view);
    void CloseItself();
  }
}