using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Lock;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Attack
{
  public class HeroAttackSystem : IEcsRunSystem
  {
    private readonly ITimerFactory _timers;
    private readonly EcsWorld _game;
    private readonly EcsFilter _heroes;
    private readonly HeroConfig _config;

    public HeroAttackSystem(GameWorldWrapper gameWorldWrapper, ITimerFactory timers, IConfigProvider configProvider)
    {
      _timers = timers;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _heroes = _game
        .Filter<HeroTag>()
        .Inc<AttackCommand>()
        .Inc<ComboAttackCounter>()
        .Inc<MovementVector>()
        .End();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (int hero in _heroes)
      {
        if(_game.Has<ComboAttackTimer>(hero))
          _game.Del<ComboAttackTimer>(hero);
        
        ref ComboAttackCounter counter = ref _game.Get<ComboAttackCounter>(hero);

        ref HitTimer hitTimer = ref _game.Add<HitTimer>(hero);
        hitTimer.TimeLeft = _timers.Create(_config.HitDurations[counter.Count]);

        _game.Add<LockMovementCommand>(hero);

        ref MovementVector vector = ref _game.Get<MovementVector>(hero);
        vector.Speed.x = 0;
        
        _game.Add<OnAttackStarted>(hero);
        _game.Add<Attacking>(hero);
        
        _game.Del<AttackCommand>(hero);
      }
    }
  }
}