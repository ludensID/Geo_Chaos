namespace LudensClub.GeoChaos.Editor.Watchers
{
  public interface IWatcher
  {
    public bool IsDifferent();
    public void Assign();
    public void OnChanged();
  }
}