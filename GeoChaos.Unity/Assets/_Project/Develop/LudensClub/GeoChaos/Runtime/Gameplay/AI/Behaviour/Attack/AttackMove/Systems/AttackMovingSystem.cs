using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.View;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Attack.AttackMove
{
  public class AttackMovingSystem<TFilterComponent> : IEcsRunSystem
    where TFilterComponent : struct, IEcsComponent
  {
    private readonly ITimerFactory _timers;
    private readonly ISpeedForceFactory _forceFactory;
    private readonly EcsWorld _game;
    private readonly EcsEntities _movingEntities;
    private readonly EcsEntities _heroes;

    public AttackMovingSystem(GameWorldWrapper gameWorldWrapper,
      ITimerFactory timers,
      ISpeedForceFactory forceFactory)
    {
      _timers = timers;
      _forceFactory = forceFactory;
      _game = gameWorldWrapper.World;

      _heroes = _game
        .Filter<HeroTag>()
        .Collect();
      
      _movingEntities = _game
        .Filter<TFilterComponent>()
        .Inc<AttackMoveCommand>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity hero in _heroes)
      foreach (EcsEntity entity in _movingEntities)
      {
        entity
          .Del<AttackMoveCommand>()
          .Add<AttackMoving>()
          .Add((ref AttackMoveTimer timer) => timer.TimeLeft = _timers.Create(entity.Get<AttackMoveTime>().Time));

        float heroPoint = hero.Get<ViewRef>().View.transform.position.x;
        float zombiePoint = entity.Get<ViewRef>().View.transform.position.x;
        float direction = Mathf.Sign(heroPoint - zombiePoint);

        _forceFactory.Create(new SpeedForceData(SpeedForceType.Move, entity.PackedEntity, Vector2.right)
        {
          Speed =  Vector2.right * entity.Get<AttackMoveSpeed>().Speed,
          Direction = Vector2.right * direction
        });

        entity.Change((ref BodyDirection bodyDirection) => bodyDirection.Direction = direction);
      }
    }
  }
}