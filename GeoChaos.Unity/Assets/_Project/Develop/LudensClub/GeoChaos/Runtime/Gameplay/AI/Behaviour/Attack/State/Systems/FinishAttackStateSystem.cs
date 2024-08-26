using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Damage;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Attack.State
{
  public class FinishAttackStateSystem<TFilterComponent> : IEcsRunSystem
    where TFilterComponent : struct, IEcsComponent
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _attackingZombies;

    public FinishAttackStateSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _attackingZombies = _game
        .Filter<TFilterComponent>()
        .Inc<FinishAttackCommand>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity zombie in _attackingZombies)
      {
        zombie
          .Del<FinishAttackCommand>()
          .Del<Attacking>()
          .Add<OnAttackFinished>();
      }
    }
  }
}