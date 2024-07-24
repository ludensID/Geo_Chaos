using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Creation
{
  public class CreateEntityWithViewSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsWorld _message;
    private readonly EcsEntities _creationMessages;
    private readonly EcsEntity _createdEntity;

    public CreateEntityWithViewSystem(GameWorldWrapper gameWorldWrapper, MessageWorldWrapper messageWorldWrapper)
    {
      _game = gameWorldWrapper.World;
      _message = messageWorldWrapper.World;

      _creationMessages = _message
        .Filter<CreateEntityMessage>()
        .Collect();

      _createdEntity = new EcsEntity(_game, -1);
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity message in _creationMessages)
      {
        ref CreateEntityMessage createMessage = ref message.Get<CreateEntityMessage>();
        if (!createMessage.Entity.TryUnpackEntity(_game, _createdEntity))
        {
          _createdEntity.Entity = _game.NewEntity();
        }
        
        createMessage.Converter.CreateEntity(_createdEntity);
        _createdEntity.Add<OnConverted>();
        
        message.Del<CreateEntityMessage>();
      }
    }
  }
}