using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Debugging.Monitoring
{
  public interface IEcsEntityPresenter
  {
    int Entity { get; }
    EcsEntityView View { get; }

    void Tick();
    void Initialize();
    void SetActive(bool value);
    void UpdateView();
    void AddComponents();
    void RemoveComponents();
    void ChangeComponent(IEcsComponent component);
  }
}