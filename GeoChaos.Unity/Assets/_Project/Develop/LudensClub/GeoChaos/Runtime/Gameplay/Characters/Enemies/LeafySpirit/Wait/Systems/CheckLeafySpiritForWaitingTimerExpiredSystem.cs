using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Wait
{
  public class CheckLeafySpiritForWaitingTimerExpiredSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _waitingSpirits;

    public CheckLeafySpiritForWaitingTimerExpiredSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _waitingSpirits = _game
        .Filter<LeafySpiritTag>()
        .Inc<WaitingTimer>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity spirit in _waitingSpirits
        .Check<WaitingTimer>(x => x.TimeLeft <= 0))
      {
        spirit.Del<WaitingTimer>();
      }
    }
  }
}