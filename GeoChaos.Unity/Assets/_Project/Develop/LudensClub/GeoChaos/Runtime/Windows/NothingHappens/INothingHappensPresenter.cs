namespace LudensClub.GeoChaos.Runtime.Windows.NothingHappens
{
  public interface INothingHappensPresenter : IWindowController
  {
    void SetView(NothingHappensView view);
    void CloseItself();
  }
}