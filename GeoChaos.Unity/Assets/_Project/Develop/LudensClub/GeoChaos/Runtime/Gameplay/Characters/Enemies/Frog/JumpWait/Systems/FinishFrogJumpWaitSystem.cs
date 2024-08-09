using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.JumpWait
{
  public class FinishFrogJumpWaitSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _waitingFrogs;

    public FinishFrogJumpWaitSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _waitingFrogs = _game
        .Filter<FrogTag>()
        .Inc<JumpWaitTimer>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity frog in _waitingFrogs
        .Check<JumpWaitTimer>(x => x.TimeLeft <= 0))
      {
        frog
          .Del<JumpWaitTimer>()
          .Add<OnJumpWaitFinished>();
      }
    }
  }
}