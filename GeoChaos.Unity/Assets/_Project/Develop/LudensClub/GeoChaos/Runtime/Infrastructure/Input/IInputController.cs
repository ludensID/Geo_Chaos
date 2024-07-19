namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public interface IInputController
  {
    void Clear();
    void HandleInput();
    void EnableGameplayMap(bool enable);
  }
}