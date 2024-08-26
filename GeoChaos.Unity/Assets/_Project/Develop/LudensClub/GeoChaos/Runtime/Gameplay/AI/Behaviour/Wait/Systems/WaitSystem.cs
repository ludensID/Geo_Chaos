using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Wait
{
  public class WaitSystem<TFilterComponent> : IEcsRunSystem
    where TFilterComponent : struct, IEcsComponent
  {
    private readonly ITimerFactory _timers;
    private readonly EcsWorld _game;
    private readonly EcsEntities _waitingEntities;

    public WaitSystem(GameWorldWrapper gameWorldWrapper, ITimerFactory timers)
    {
      _timers = timers;
      _game = gameWorldWrapper.World;

      _waitingEntities = _game
        .Filter<TFilterComponent>()
        .Inc<WaitCommand>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity entity in _waitingEntities)
      {
        entity
          .Del<WaitCommand>()
          .Add((ref WaitingTimer timer) => timer.TimeLeft = _timers.Create(entity.Get<WaitTime>().Time));
      }
    }
  }
}