using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Lock;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Attack
{
  public class StopHeroAttackSystem : IEcsRunSystem
  {
    private readonly ITimerFactory _timers;
    private readonly EcsWorld _game;
    private readonly HeroConfig _config;
    private readonly EcsEntities _heroes;

    public StopHeroAttackSystem(GameWorldWrapper gameWorldWrapper, ITimerFactory timers, IConfigProvider configProvider)
    {
      _timers = timers;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _heroes = _game
        .Filter<HeroTag>()
        .Inc<HitTimer>()
        .Inc<Attacking>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity hero in _heroes
        .Where<HitTimer>(x => x.TimeLeft <= 0))
      {
        hero
          .Del<HitTimer>()
          .Del<Attacking>()
          .Add<OnAttackFinished>()
          .Add<UnlockMovementCommand>();

        ref ComboAttackCounter counter = ref hero.Get<ComboAttackCounter>();
        counter.Count++;
        counter.Count %= 3;

        if (counter.Count != 0)
        {
          int count = counter.Count;
          hero.Add((ref ComboAttackTimer timer) =>
            timer.TimeLeft = _timers.Create(_config.ComboAttackPeriods[count - 1]));
        }
      }
    }
  }
}