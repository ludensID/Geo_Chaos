using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Damage;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Immunity
{
  public class TakeHeroImmunitySystem : IEcsRunSystem
  {
    private readonly ITimerFactory _timers;
    private readonly EcsWorld _game;
    private readonly EcsWorld _message;
    private readonly HeroConfig _config;
    private readonly EcsEntities _damagedEvents;

    public TakeHeroImmunitySystem(GameWorldWrapper gameWorldWrapper,
      MessageWorldWrapper messageWorldWrapper,
      IConfigProvider configProvider,
      ITimerFactory timers)
    {
      _timers = timers;
      _game = gameWorldWrapper.World;
      _message = messageWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _damagedEvents = _message
        .Filter<OnDamaged>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity damaged in _damagedEvents)
      {
        if(damaged.Get<OnDamaged>().Target.TryUnpackEntity(_game, out EcsEntity hero)  
          && hero.Has<HeroTag>() && hero.Has<Immune>())
        {
          hero.Add((ref ImmunityTimer timer) => timer.TimeLeft = _timers.Create(_config.ImmunityTime));
        }
      }
    }
  }
}