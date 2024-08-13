using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.AttackWait
{
  public class DeleteExpiredFrogAttackWaitingTimerSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _waitingFrogs;

    public DeleteExpiredFrogAttackWaitingTimerSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _waitingFrogs = _game
        .Filter<FrogTag>()
        .Inc<AttackWaitingTimer>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity frog in _waitingFrogs
        .Check<AttackWaitingTimer>(x => x.TimeLeft <= 0))
      {
        frog.Del<AttackWaitingTimer>();
      }
    }
  }
}