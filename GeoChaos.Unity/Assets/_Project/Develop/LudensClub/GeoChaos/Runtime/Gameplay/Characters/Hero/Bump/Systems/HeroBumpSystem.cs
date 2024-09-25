using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Bump;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Damage;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.View;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Bump
{
  public class HeroBumpSystem : IEcsRunSystem
  {
    private readonly ISpeedForceFactory _forceFactory;
    private readonly ITimerFactory _timers;
    private readonly EcsWorld _game;
    private readonly EcsWorld _message;
    private readonly EcsWorld _physics;
    private readonly EcsEntities _damageEvents;
    private readonly HeroConfig _config;
    private readonly EcsEntities _forces;

    public HeroBumpSystem(GameWorldWrapper gameWorldWrapper,
      MessageWorldWrapper messageWorldWrapper,
      PhysicsWorldWrapper physicsWorldWrapper,
      ISpeedForceFactory forceFactory,
      IConfigProvider configProvider,
      ITimerFactory timers)
    {
      _forceFactory = forceFactory;
      _timers = timers;
      _game = gameWorldWrapper.World;
      _message = messageWorldWrapper.World;
      _physics = physicsWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _damageEvents = _message
        .Filter<OnDamaged>()
        .Collect();

      _forces = _physics
        .Filter<Owner>()
        .Inc<SpeedForce>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity damage in _damageEvents)
      {
        ref DamageInfo damaged = ref damage.Get<OnDamaged>().Info;
        if (damaged.Target.TryUnpackEntity(_game, out EcsEntity hero)
          && hero.Has<HeroTag>()
          && hero.Has<BumpAvailable>())
        {
          Vector2 direction = Vector2.zero;
          if (damaged.BumpPosition != Vector3.zero)
          {
            Vector3 masterPosition = damaged.BumpPosition;
            Vector3 heroPosition = hero.Get<ViewRef>().View.transform.position;
            
            direction = new Vector2(Mathf.Sign(heroPosition.x - masterPosition.x), 1);
          }

          if (direction == Vector2.zero)
            direction = new Vector2(-hero.Get<BodyDirection>().Direction, 1);

          foreach (EcsEntity force in _forces
            .Check<Owner>(x => x.Entity.EqualsTo(hero.PackedEntity)))
          {
            force.Dispose();
          }

          _forceFactory.Create(new SpeedForceData(SpeedForceType.Bump, hero.PackedEntity, Vector2.one)
          {
            Speed = _config.BumpForce,
            Direction = direction,
            Instant = true
          });

          hero
            .Has<Bumping>(true)
            .Has<BodyFreezing>(true)
            .Change((ref MovementLayout layout) =>
            {
              layout.Layer = MovementLayer.Shoot;
              layout.Movement = MovementType.Bump;
            })
            .Replace((ref BumpTimer timer) => timer.TimeLeft = _timers.Create(_config.BumpFreezeDuration));
        }
      }
    }
  }
}