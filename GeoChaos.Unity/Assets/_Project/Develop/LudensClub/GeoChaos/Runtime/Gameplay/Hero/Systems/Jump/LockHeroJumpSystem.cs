using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Lock;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Jump
{
  public class LockHeroJumpSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsFilter _jumpings;

    public LockHeroJumpSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _jumpings = _game
        .Filter<IsJumping>()
        .Inc<OnMovementLocked>()
        .End();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (int jumping in _jumpings)
        _game.Del<IsJumping>(jumping);
    }
  }
}