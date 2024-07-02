using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Immunity
{
  public class TakeHeroImmunitySystem : IEcsRunSystem
  {
    private readonly ITimerFactory _timers;
    private readonly EcsWorld _game;
    private readonly EcsEntities _immuneHeroes;
    private readonly HeroConfig _config;

    public TakeHeroImmunitySystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider, ITimerFactory timers)
    {
      _timers = timers;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _immuneHeroes = _game
        .Filter<HeroTag>()
        .Inc<Immune>()
        .Exc<ImmunityTimer>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity hero in _immuneHeroes)
      {
        hero.Add((ref ImmunityTimer timer) => timer.TimeLeft = _timers.Create(_config.ImmunityTime));
      }
    }
  }
}