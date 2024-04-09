namespace LudensClub.GeoChaos.Debugging.Monitoring
{
  public interface IEcsEntityPresenter
  {
    int Entity { get; }
    EcsEntityView View { get; }

    void Tick();
    void Initialize();
    void SetActive(bool value);
  }
}