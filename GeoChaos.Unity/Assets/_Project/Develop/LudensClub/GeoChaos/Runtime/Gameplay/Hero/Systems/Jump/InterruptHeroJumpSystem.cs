using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Lock;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Jump
{
  public class InterruptHeroJumpSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsFilter _jumpings;

    public InterruptHeroJumpSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _jumpings = _game
        .Filter<Jumping>()
        .Inc<OnMovementLocked>()
        .End();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (int jumping in _jumpings)
        _game.Del<Jumping>(jumping);
    }
  }
}