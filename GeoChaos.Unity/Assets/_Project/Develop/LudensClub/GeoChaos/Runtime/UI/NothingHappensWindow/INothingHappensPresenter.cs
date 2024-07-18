namespace LudensClub.GeoChaos.Runtime.UI.NothingHappensWindow
{
  public interface INothingHappensPresenter
  {
    void SetView(NothingHappensView view);
    void ShowWindow();
    void CloseWindow();
  }
}