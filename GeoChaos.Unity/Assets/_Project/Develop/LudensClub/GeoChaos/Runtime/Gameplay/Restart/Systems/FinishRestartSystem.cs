using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Restart
{
  public class FinishRestartSystem : IEcsRunSystem
  {
    private readonly IEcsRestartService _restartSvc;
    private readonly EcsWorld _message;
    private readonly EcsEntities _heroRestartedEvents;

    public FinishRestartSystem(MessageWorldWrapper messageWorldWrapper, IEcsRestartService restartSvc)
    {
      _restartSvc = restartSvc;
      _message = messageWorldWrapper.World;
      
      _heroRestartedEvents = _message
        .Filter<OnHeroRestarted>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity restart in _heroRestartedEvents)
      {
        restart.Dispose();
        _restartSvc.FinishRestart();
      }
    }
  }
}