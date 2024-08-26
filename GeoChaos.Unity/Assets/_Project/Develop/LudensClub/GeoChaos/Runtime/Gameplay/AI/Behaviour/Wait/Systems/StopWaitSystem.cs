using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Wait
{
  public class StopWaitSystem<TFilterComponent> : IEcsRunSystem
    where TFilterComponent : struct, IEcsComponent
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _waitingEntities;

    public StopWaitSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _waitingEntities = _game
        .Filter<TFilterComponent>()
        .Inc<StopWaitCommand>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity entity in _waitingEntities)
      {
        entity
          .Del<StopWaitCommand>()
          .Has<WaitingTimer>(false);
      }
    }
  }
}