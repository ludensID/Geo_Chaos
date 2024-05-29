using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Characteristics.Components;
using LudensClub.GeoChaos.Runtime.Gameplay.Creation.Components;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class CreateHeroEntitySystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsWorld _message;
    private readonly EcsFilter _commands;

    public CreateHeroEntitySystem(GameWorldWrapper worldWrapper)
    {
      _game = worldWrapper.World;

      _commands = _game
        .Filter<CreateCommand>()
        .Inc<EntityId>()
        .End();
    }

    public void Run(EcsSystems systems)
    {
      foreach (var command in _commands
        .Where<EntityId>(x => x.Id == EntityType.Hero))
      {
        _game.Add<HeroTag>(command);

        ref var health = ref _game.Add<Health>(command);
        health.Value = 100;

        _game.Del<CreateCommand>(command);
        _game.Add<InitializeCommand>(command);
      }
    }
  }
}