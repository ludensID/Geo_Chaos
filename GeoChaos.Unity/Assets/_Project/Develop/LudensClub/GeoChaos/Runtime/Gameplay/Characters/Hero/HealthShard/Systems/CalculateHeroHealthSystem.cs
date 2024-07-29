using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Characteristics;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.HealthShard
{
  public class CalculateHeroHealthSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _calculatedHealthHeroes;
    private readonly HeroConfig _config;

    public CalculateHeroHealthSystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _calculatedHealthHeroes = _game
        .Filter<HeroTag>()
        .Inc<OnHealthCalculated>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity hero in _calculatedHealthHeroes)
      {
        ref MaxCurrentHealth maxHealth = ref hero.Get<MaxCurrentHealth>();
        maxHealth.Health += (int)(hero.Get<HealthShardCounter>().Count / 3.0 * _config.HealthShardPoint);
      }
    }
  }
}