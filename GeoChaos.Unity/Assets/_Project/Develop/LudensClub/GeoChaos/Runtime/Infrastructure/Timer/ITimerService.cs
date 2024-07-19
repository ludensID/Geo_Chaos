namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public interface ITimerService
  {
    void AddTimer(ITimerable elem, bool unscaled = false);
    void RemoveTimer(ITimerable elem);
  }
}