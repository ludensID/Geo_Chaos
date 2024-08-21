using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.ArmsAttack
{
  public class EnableArmsColliderSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _startAttackZombies;
    private readonly EcsEntities _finishAttackZombies;

    public EnableArmsColliderSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _startAttackZombies = _game
        .Filter<ZombieTag>()
        .Inc<OnAttackWithArmsStarted>()
        .Collect();
      
      _finishAttackZombies = _game
        .Filter<ZombieTag>()
        .Inc<OnAttackWithArmsFinished>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity zombie in _startAttackZombies)
      {
        EnableArmsCollider(zombie, true);
      }

      foreach (EcsEntity zombie in _finishAttackZombies)
      {
        EnableArmsCollider(zombie, false);
      }
    }

    private void EnableArmsCollider(EcsEntity entity, bool enabled)
    {
      entity.Change((ref ArmsColliderRef armsColliderRef) => armsColliderRef.Collider.enabled = enabled);
    }
  }
}