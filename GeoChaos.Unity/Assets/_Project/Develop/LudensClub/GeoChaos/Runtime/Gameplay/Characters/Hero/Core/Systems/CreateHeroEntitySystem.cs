using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Characteristics;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Creation;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Systems
{
  public class CreateHeroEntitySystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsWorld _message;
    private readonly EcsEntities _commands;
    private readonly HeroConfig _config;

    public CreateHeroEntitySystem(GameWorldWrapper worldWrapper, IConfigProvider configProvider)
    {
      _game = worldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _commands = _game
        .Filter<OnConverted>()
        .Inc<EntityId>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity command in _commands
        .Check<EntityId>(x => x.Id == EntityType.Hero))
      {
        command
          .Change((ref CurrentHealth health) => health.Health = _config.Health)
          .Change((ref MaxCurrentHealth maxHealth) => maxHealth.Health = _config.Health)
          .Change((ref DefaultHealth defaultHealth) => defaultHealth.Health = _config.Health);
      }
    }
  }
}