using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Restart
{
  public class CleanSceneBeforeRestartSystem : IEcsRunSystem
  {
    private readonly EcsWorld _message;
    private readonly EcsEntities _restartMessages;

    public CleanSceneBeforeRestartSystem(MessageWorldWrapper messageWorldWrapper)
    {
      _message = messageWorldWrapper.World;

      _restartMessages = _message
        .Filter<RestartLevelMessage>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity restart in _restartMessages)
      {
        restart
          .Del<RestartLevelMessage>()
          .Add<BeforeRestartMessage>();
      }
    }
  }
}