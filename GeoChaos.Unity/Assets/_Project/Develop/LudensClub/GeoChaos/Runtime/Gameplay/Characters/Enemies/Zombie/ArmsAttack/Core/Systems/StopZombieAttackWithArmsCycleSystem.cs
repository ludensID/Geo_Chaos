using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.ArmsAttack
{
  public class StopZombieAttackWithArmsCycleSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _attackingZombies;

    public StopZombieAttackWithArmsCycleSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _attackingZombies = _game
        .Filter<ZombieTag>()
        .Inc<StopAttackWithArmsCycleCommand>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity zombie in _attackingZombies)
      {
        zombie
          .Del<StopAttackWithArmsCycleCommand>()
          .Has<AttackingWithArms>(false)
          .Has<AttackWithArmsTimer>(false)
          .Has<AttackWithArmsCooldown>(false)
          .Has<OnAttackWithArmsFinished>(true);
      }
    }
  }
}