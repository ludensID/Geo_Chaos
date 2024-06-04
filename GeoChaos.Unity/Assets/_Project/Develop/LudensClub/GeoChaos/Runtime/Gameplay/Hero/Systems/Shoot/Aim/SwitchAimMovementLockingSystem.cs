using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Lock;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Shoot.Aim;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Shoot.Aim
{
  public class SwitchAimMovementLockingSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _startedAimEvents;
    private readonly EcsEntities _finishedAimEvents;

    public SwitchAimMovementLockingSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _startedAimEvents = _game
        .Filter<OnAimStarted>()
        .Collect();

      _finishedAimEvents = _game
        .Filter<OnAimFinished>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity started in _startedAimEvents)
      {
        started.Add<LockMovementCommand>();
      }

      foreach (EcsEntity finished in _finishedAimEvents)
      {
        finished.Add<UnlockMovementCommand>();
      }
    }
  }
}