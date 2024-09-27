using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Restart
{
  public class WaitFinishRestartSystem : IEcsRunSystem
  {
    private readonly EcsWorld _message;
    private readonly EcsEntities _restartMessages;

    public WaitFinishRestartSystem(MessageWorldWrapper messageWorldWrapper)
    {
      _message = messageWorldWrapper.World;

      _restartMessages = _message
        .Filter<OnRestartMessage>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity restart in _restartMessages)
      {
        restart
          .Add<AfterRestartMessage>()
          .Del<OnRestartMessage>();
      }
    }
  }
}