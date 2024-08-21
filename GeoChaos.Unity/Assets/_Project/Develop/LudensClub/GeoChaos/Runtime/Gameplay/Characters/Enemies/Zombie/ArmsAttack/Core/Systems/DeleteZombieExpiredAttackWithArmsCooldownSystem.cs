using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.ArmsAttack
{
  public class DeleteZombieExpiredAttackWithArmsCooldownSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _attackingZombies;

    public DeleteZombieExpiredAttackWithArmsCooldownSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _attackingZombies = _game
        .Filter<ZombieTag>()
        .Inc<AttackWithArmsCooldown>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity zombie in _attackingZombies
        .Check<AttackWithArmsCooldown>(x => x.TimeLeft <= 0))
      {
        zombie.Del<AttackWithArmsCooldown>();
      }
    }
  }
}