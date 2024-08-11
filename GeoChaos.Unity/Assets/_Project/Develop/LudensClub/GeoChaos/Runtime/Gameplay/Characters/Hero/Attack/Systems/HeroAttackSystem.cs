using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Damage;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Attack
{
  public class HeroAttackSystem : IEcsRunSystem
  {
    private readonly ITimerFactory _timers;
    private readonly ISpeedForceFactory _forceFactory;
    private readonly EcsWorld _game;
    private readonly HeroConfig _config;
    private readonly EcsEntities _heroes;

    public HeroAttackSystem(GameWorldWrapper gameWorldWrapper,
      ITimerFactory timers,
      IConfigProvider configProvider,
      ISpeedForceFactory forceFactory)
    {
      _timers = timers;
      _forceFactory = forceFactory;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _heroes = _game
        .Filter<HeroTag>()
        .Inc<AttackCommand>()
        .Inc<ComboAttackCounter>()
        .Inc<MovementVector>()
        .Exc<HitTimer>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity hero in _heroes)
      {
        int count = hero.Get<ComboAttackCounter>().Count;
        hero.Has<ComboAttackTimer>(false)
          .Add((ref HitTimer hitTimer) => hitTimer.TimeLeft = _timers.Create(_config.HitDurations[count]))
          .Change((ref MovementLayout layout) =>
          {
            layout.Layer = MovementLayer.None;
            layout.Owner = MovementType.Attack;
          })
          .Add<OnAttackStarted>()
          .Add<Attacking>()
          .Del<AttackCommand>();
        
        _forceFactory.Create(new SpeedForceData(SpeedForceType.Attack, hero.Pack(), Vector2.right)
        {
          Instant = true
        });
      }
    }
  }
}