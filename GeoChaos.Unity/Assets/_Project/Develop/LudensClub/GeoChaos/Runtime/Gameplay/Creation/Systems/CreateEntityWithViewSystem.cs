using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Creation.Components;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Creation
{
  public class CreateEntityWithViewSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsWorld _message;
    private readonly EcsEntities _creationMessages;

    public CreateEntityWithViewSystem(GameWorldWrapper gameWorldWrapper, MessageWorldWrapper messageWorldWrapper)
    {
      _game = gameWorldWrapper.World;
      _message = messageWorldWrapper.World;

      _creationMessages = _message
        .Filter<CreateMonoEntityMessage>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity message in _creationMessages)
      {
        MonoGameObjectConverter converter = message.Get<CreateMonoEntityMessage>().Converter;
        EcsEntity instance = _game.CreateEntity()
          .Add((ref EntityId id) => id.Id = converter.Id)
          .Add<CreateCommand>();

        converter.Convert(instance);
        instance.Add<OnConverted>();

        message.Del<CreateMonoEntityMessage>();
      }
    }
  }
}