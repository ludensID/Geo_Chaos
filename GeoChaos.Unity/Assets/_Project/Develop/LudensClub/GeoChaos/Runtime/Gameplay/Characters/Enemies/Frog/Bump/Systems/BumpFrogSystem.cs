using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Bump;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Jump;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Damage;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.View;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Bump
{
  public class BumpFrogSystem : IEcsRunSystem
  {
    private readonly ISpeedForceFactory _forceFactory;
    private readonly EcsWorld _message;
    private readonly EcsWorld _game;
    private readonly FrogConfig _config;
    private readonly EcsEntities _damageEvents;

    public BumpFrogSystem(MessageWorldWrapper messageWorldWrapper,
      GameWorldWrapper gameWorldWrapper,
      IConfigProvider configProvider,
      ISpeedForceFactory forceFactory)
    {
      _forceFactory = forceFactory;
      _message = messageWorldWrapper.World;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<FrogConfig>();

      _damageEvents = _message
        .Filter<OnDamaged>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity entity in _damageEvents)
      {
        ref OnDamaged damage = ref entity.Get<OnDamaged>();
        if (damage.Target.TryUnpackEntity(_game, out EcsEntity frog) 
          && frog.Has<FrogTag>() 
          && !frog.Has<Jumping>() 
          && !frog.Has<Bumping>())
        {
           Vector2 direction = Vector2.zero;
           if (damage.Master.TryUnpackEntity(_game, out EcsEntity master) && master.Has<ViewRef>())
           {
             float masterPoint = master.Get<ViewRef>().View.transform.position.x;
             float frogPoint = frog.Get<ViewRef>().View.transform.position.x;

             direction = new Vector2(Mathf.Sign(masterPoint - frogPoint), 1);
           }

           if (direction == Vector2.zero)
             direction = new Vector2(-frog.Get<BodyDirection>().Direction, 1);
           
           _forceFactory.Create(new SpeedForceData(SpeedForceType.Bump, frog.PackedEntity, Vector2.one)
           {
             Speed = _config.BumpForce,
             Direction = direction,
             Instant = true
           });

           frog.Add<Bumping>();
        }
      }
    }
  }
}