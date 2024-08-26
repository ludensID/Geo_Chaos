using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Damage;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Attack.Cooldown
{
  public class DeleteExpiredAttackCooldownSystem<TFilterComponent> : IEcsRunSystem
    where TFilterComponent : struct, IEcsComponent
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _zombies;

    public DeleteExpiredAttackCooldownSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _zombies = _game
        .Filter<TFilterComponent>()
        .Inc<AttackCooldown>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity zombie in _zombies
        .Check<AttackCooldown>(x => x.TimeLeft <= 0))
      {
        zombie.Del<AttackCooldown>();
      }
    }
  }
}