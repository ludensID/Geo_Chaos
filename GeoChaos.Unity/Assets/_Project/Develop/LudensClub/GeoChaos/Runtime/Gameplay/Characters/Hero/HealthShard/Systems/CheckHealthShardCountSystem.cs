using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Characteristics;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.HealthShard
{
  public class CheckHealthShardCountSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _heroShards;

    public CheckHealthShardCountSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _heroShards = _game
        .Filter<HeroTag>()
        .Inc<HealthShardCounter>()
        .Inc<OnHealthShardTaken>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity hero in _heroShards)
      {
        ref HealthShardCounter counter = ref hero.Get<HealthShardCounter>();
        if (counter.Count % 3 == 0)
          hero.Add<CalculateHealthCommand>();
      }
    }
  }
}