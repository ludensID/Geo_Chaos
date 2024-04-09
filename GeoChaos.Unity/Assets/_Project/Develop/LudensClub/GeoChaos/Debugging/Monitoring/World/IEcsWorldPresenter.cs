namespace LudensClub.GeoChaos.Debugging.Monitoring
{
  public interface IEcsWorldPresenter
  {
    EcsWorldView View { get; }
    void Initialize();
    void Tick();
  }
}