using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Attack.AttackMove;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.ArmsAttack;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Attack.Preparing;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Attack.AttackMove
{
  public class StartZombieAttackMoveSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _movingZombies;

    public StartZombieAttackMoveSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _movingZombies = _game
        .Filter<ZombieTag>()
        .Inc<OnAttackPreparingFinished>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity zombie in _movingZombies)
      {
        zombie
          .Add<AttackMoveCommand>()
          .Add<StartAttackWithArmsCycleCommand>();
      }
    }
  }
}