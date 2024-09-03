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
    private readonly EcsWorld _physics;
    private readonly EcsEntities _forces;
    private readonly BelongOwnerClosure _belongOwnerClosure = new BelongOwnerClosure();

    public HeroAttackSystem(GameWorldWrapper gameWorldWrapper,
      PhysicsWorldWrapper physicsWorldWrapper,
      ITimerFactory timers,
      IConfigProvider configProvider,
      ISpeedForceFactory forceFactory)
    {
      _timers = timers;
      _forceFactory = forceFactory;
      _game = gameWorldWrapper.World;
      _physics = physicsWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _heroes = _game
        .Filter<HeroTag>()
        .Inc<AttackCommand>()
        .Inc<ComboAttackCounter>()
        .Inc<MovementVector>()
        .Exc<HitTimer>()
        .Collect();

      _forces = _physics
        .Filter<SpeedForce>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity hero in _heroes)
      {
        int count = hero.Get<ComboAttackCounter>().Count;
        hero
          .Has<ComboAttackTimer>(false)
          .Add((ref HitTimer hitTimer) => hitTimer.TimeLeft = _timers.Create(_config.HitDurations[count]))
          .Change((ref MovementLayout layout) =>
          {
            layout.Layer = MovementLayer.None;
            layout.Owner = MovementType.Attack;
          })
          .Add<OnAttackStarted>()
          .Add<Attacking>();

        foreach (EcsEntity force in _forces
          .Check(_belongOwnerClosure.SpecifyPredicate(hero.PackedEntity)))
        {
          force.Dispose();
        }

        _forceFactory.Create(new SpeedForceData(SpeedForceType.Attack, hero.PackedEntity, Vector2.one)
        {
          Instant = true
        });
      }
    }
  }
}