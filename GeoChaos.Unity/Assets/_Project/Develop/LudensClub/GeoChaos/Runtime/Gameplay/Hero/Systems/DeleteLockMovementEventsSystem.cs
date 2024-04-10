using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Lock;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems
{
  public class DeleteLockMovementEventsSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsFilter _onLockeds;
    private readonly EcsFilter _onUnlockeds;

    public DeleteLockMovementEventsSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _onLockeds = _game
        .Filter<OnMovementLocked>()
        .End();

      _onUnlockeds = _game
        .Filter<OnMovementUnlocked>()
        .End();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (int onLocked in _onLockeds)
        _game.Del<OnMovementLocked>(onLocked);

      foreach (int onUnlocked in _onUnlockeds)
        _game.Del<OnMovementUnlocked>(onUnlocked);
    }
  }
}