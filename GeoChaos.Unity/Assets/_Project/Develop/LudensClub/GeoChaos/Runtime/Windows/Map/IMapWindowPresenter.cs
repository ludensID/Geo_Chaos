namespace LudensClub.GeoChaos.Runtime.Windows.Map
{
  public interface IMapWindowPresenter : IWindowController, ICloseHandler
  {
    void SetView(MapWindowView view);
  }
}