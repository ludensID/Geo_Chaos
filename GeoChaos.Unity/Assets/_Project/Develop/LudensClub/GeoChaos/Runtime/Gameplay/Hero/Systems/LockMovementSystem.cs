using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Lock;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems
{
  public class LockMovementSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _lockCommands;
    private readonly EcsEntities _unlockCommands;

    public LockMovementSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _lockCommands = _game
        .Filter<LockMovementCommand>()
        .Collect();

      _unlockCommands = _game
        .Filter<UnlockMovementCommand>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity lockCommand in _lockCommands)
      {
        lockCommand
          .Add<OnMovementLocked>()
          .Add<MovementLocked>()
          .Del<LockMovementCommand>();
      }

      foreach (EcsEntity unlockCommand in _unlockCommands)
      {
        unlockCommand
          .Add<OnMovementUnlocked>()
          .Del<MovementLocked>()
          .Del<UnlockMovementCommand>();
      }
    }
  }
}