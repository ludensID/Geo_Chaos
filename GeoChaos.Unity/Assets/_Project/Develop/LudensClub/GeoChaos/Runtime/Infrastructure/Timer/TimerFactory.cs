namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public class TimerFactory : ITimerFactory
  {
    private readonly ITimerService _timerSvc;

    public TimerFactory(ITimerService timerSvc)
    {
      _timerSvc = timerSvc;
    }

    public Timer Create(float time) 
    {
      Timer instance = time; 
      _timerSvc.AddTimer(instance);
      return instance;
    }
  }
}