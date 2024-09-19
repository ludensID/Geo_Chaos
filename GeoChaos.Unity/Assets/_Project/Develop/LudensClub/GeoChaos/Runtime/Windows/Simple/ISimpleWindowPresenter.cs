 namespace LudensClub.GeoChaos.Runtime.Windows.Simple
{
  public interface ISimpleWindowPresenter : IWindowController, ICloseHandler
  {
    void SetView(SimpleWindowView view);
  }
}