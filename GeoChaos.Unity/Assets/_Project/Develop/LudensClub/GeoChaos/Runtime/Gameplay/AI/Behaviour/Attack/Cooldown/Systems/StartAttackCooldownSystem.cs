using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Damage;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Attack.Cooldown
{
  public class StartAttackCooldownSystem<TFilterComponent> : IEcsRunSystem
    where TFilterComponent : struct, IEcsComponent
  {
    private readonly ITimerFactory _timers;
    private readonly EcsWorld _game;
    private readonly EcsEntities _attackingEntity;

    public StartAttackCooldownSystem(GameWorldWrapper gameWorldWrapper, ITimerFactory timers)
    {
      _timers = timers;
      _game = gameWorldWrapper.World;

      _attackingEntity = _game
        .Filter<TFilterComponent>()
        .Inc<OnAttackFinished>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity entity in _attackingEntity)
      {
        entity.Add((ref AttackCooldown cooldown) => cooldown.TimeLeft = _timers.Create(entity.Get<AttackCooldownTime>().Time));
      }
    }
  }
}