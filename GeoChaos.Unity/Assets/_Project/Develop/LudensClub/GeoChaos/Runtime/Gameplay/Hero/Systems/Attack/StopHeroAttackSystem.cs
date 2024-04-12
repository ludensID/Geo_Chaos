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
    private readonly EcsFilter _heroes;
    private readonly HeroConfig _config;

    public StopHeroAttackSystem(GameWorldWrapper gameWorldWrapper, ITimerFactory timers, IConfigProvider configProvider)
    {
      _timers = timers;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _heroes = _game
        .Filter<HeroTag>()
        .Inc<HitTimer>()
        .Inc<IsAttacking>()
        .End();
    }

    public void Run(EcsSystems systems)
    {
      foreach (int hero in _heroes
        .Where((ref HitTimer x) => x.TimeLeft <= 0))
      {
        _game.Del<HitTimer>(hero);
        _game.Del<IsAttacking>(hero);
        _game.Add<OnAttackFinished>(hero);
        _game.Add<UnlockMovementCommand>(hero);

        ref ComboAttackCounter counter = ref _game.Get<ComboAttackCounter>(hero);
        counter.Count++;
        counter.Count %= 3;

        if (counter.Count != 0)
        {
          ref ComboAttackTimer comboAttack = ref _game.Add<ComboAttackTimer>(hero);
          comboAttack.TimeLeft = _timers.Create(_config.ComboAttackPeriods[counter.Count - 1]);
        }
      }
    }
  }
}