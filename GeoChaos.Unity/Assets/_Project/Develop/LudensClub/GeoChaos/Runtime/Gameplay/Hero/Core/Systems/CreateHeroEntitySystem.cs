using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Characteristics.Components;
using LudensClub.GeoChaos.Runtime.Gameplay.Creation.Components;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class CreateHeroEntitySystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsWorld _message;
    private readonly EcsEntities _commands;

    public CreateHeroEntitySystem(GameWorldWrapper worldWrapper)
    {
      _game = worldWrapper.World;

      _commands = _game
        .Filter<CreateCommand>()
        .Inc<EntityId>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity command in _commands
        .Where<EntityId>(x => x.Id == EntityType.Hero))
      {
        command.Add<HeroTag>()
          .Add((ref Health health) => health.Value = 100)
          .Del<CreateCommand>()
          .Add<InitializeCommand>();
      }
    }
  }
}