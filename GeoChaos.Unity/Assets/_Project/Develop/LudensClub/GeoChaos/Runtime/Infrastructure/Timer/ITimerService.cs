namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public interface ITimerService
  {
    void AddTimer(ITimerable elem);
    void RemoveTimer(ITimerable elem);
  }
}