using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.ArmsAttack;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Damage;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Attack.AttackMove
{
  public class FinishZombieAttackMovingSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _movingZombies;

    public FinishZombieAttackMovingSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _movingZombies = _game
        .Filter<ZombieTag>()
        .Inc<FinishAttackMoveCommand>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity zombie in _movingZombies)
      {
        zombie
          .Del<FinishAttackMoveCommand>()
          .Del<AttackMoving>()
          .Add<FinishAttackCommand>()
          .Add<StopAttackWithArmsCommand>();
      }
    }
  }
}