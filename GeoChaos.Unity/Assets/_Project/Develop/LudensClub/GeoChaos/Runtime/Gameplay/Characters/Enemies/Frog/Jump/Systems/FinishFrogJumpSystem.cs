using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Jump;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Gravity;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Jump
{
  public class FinishFrogJumpSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _jumpingFrogs;

    public FinishFrogJumpSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _jumpingFrogs = _game
        .Filter<FrogTag>()
        .Inc<Jumping>()
        .Inc<OnLanded>()
        .Exc<OnJumpStarted>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity frog in _jumpingFrogs)
      {
        frog
          .Del<Jumping>()
          .Add<OnJumpFinished>();
      }
    }
  }
}