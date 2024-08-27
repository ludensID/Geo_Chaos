using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.ArmsAttack
{
  public class StartZombieAttackWithArmsCycleSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _attackingZombies;

    public StartZombieAttackWithArmsCycleSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;
      
      _attackingZombies = _game 
        .Filter<ZombieTag>()
        .Inc<StartAttackWithArmsCycleCommand>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity zombie in _attackingZombies)
      {
        zombie
          .Del<StartAttackWithArmsCycleCommand>()
          .Add<AttackingWithArms>();
      }
    }
  }
}