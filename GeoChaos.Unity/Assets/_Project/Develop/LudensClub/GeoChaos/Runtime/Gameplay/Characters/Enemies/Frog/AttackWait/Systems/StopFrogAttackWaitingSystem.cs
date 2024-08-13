using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.AttackWait
{
  public class StopFrogAttackWaitingSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _waitingFrogs;

    public StopFrogAttackWaitingSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _waitingFrogs = _game
        .Filter<FrogTag>()
        .Inc<StopWaitAttackCommand>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity frog in _waitingFrogs)
      {
        frog
          .Del<StopWaitAttackCommand>()
          .Has<AttackWaitingTimer>(false);
      }
    }
  }
}