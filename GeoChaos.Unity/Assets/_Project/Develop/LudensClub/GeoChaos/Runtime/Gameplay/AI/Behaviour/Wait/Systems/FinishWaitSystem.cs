using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Wait
{
  public class FinishWaitSystem<TFilterComponent> : IEcsRunSystem
    where TFilterComponent : struct, IEcsComponent
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _waitingEntities;

    public FinishWaitSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _waitingEntities = _game
        .Filter<TFilterComponent>()
        .Inc<WaitingTimer>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity entity in _waitingEntities
        .Check<WaitingTimer>(x => x.TimeLeft <= 0))
      {
        entity.Del<WaitingTimer>();
      }
    }
  }
}