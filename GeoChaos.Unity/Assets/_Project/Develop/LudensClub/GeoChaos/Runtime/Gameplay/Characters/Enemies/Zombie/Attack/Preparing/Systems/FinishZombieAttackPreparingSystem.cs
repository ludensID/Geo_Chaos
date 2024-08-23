using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Attack.Preparing
{
  public class FinishZombieAttackPreparingSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _preparingZombies;

    public FinishZombieAttackPreparingSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _preparingZombies = _game
        .Filter<ZombieTag>()
        .Inc<FinishPrepareToAttackCommand>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity zombie in _preparingZombies)
      {
        zombie
          .Del<FinishPrepareToAttackCommand>()
          .Del<AttackPreparing>()
          .Del<AttackPreparingTimer>()
          .Add<OnAttackPreparingFinished>();
      }
    }
  }
}