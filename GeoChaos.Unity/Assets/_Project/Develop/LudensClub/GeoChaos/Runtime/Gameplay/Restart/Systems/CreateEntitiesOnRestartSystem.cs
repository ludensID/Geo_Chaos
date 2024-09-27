using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Restart
{
  public class CreateEntitiesOnRestartSystem : IEcsRunSystem
  {
    private readonly EcsWorld _message;
    private readonly EcsEntities _beforeRestartMessages;

    public CreateEntitiesOnRestartSystem(MessageWorldWrapper messageWorldWrapper)
    {
      _message = messageWorldWrapper.World;

      _beforeRestartMessages = _message
        .Filter<BeforeRestartMessage>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity restart in _beforeRestartMessages)
      {
        restart
          .Del<BeforeRestartMessage>()
          .Add<AfterRestartMessage>();
      }
    }
  }
}