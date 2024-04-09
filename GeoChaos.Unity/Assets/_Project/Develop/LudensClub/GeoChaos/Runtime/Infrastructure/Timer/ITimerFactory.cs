namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public interface ITimerFactory
  {
    Timer Create(float time);
  }
}