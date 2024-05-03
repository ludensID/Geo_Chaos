using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Lock;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems
{
  public class LockMovementSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsFilter _lockCommands;
    private readonly EcsFilter _unlockCommands;

    public LockMovementSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _lockCommands = _game
        .Filter<LockMovementCommand>()
        .End();

      _unlockCommands = _game
        .Filter<UnlockMovementCommand>()
        .End();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (int lockCommand in _lockCommands)
      {
        _game.Add<OnMovementLocked>(lockCommand);
        _game.Add<MovementLocked>(lockCommand);
        _game.Del<LockMovementCommand>(lockCommand);
      }

      foreach (int unlockCommand in _unlockCommands)
      {
        _game.Add<OnMovementUnlocked>(unlockCommand);
        _game.Del<MovementLocked>(unlockCommand);
        _game.Del<UnlockMovementCommand>(unlockCommand);
      }
    }
  }
}