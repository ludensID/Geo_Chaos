using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.JumpWait;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.JumpCycle
{
  public class RestartFrogJumpCycleSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _cyclingFrogs;

    public RestartFrogJumpCycleSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _cyclingFrogs = _game
        .Filter<FrogTag>()
        .Inc<StartJumpCycleCommand>()
        .Inc<StopJumpCycleCommand>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity frog in _cyclingFrogs)
      {
        frog
          .Del<StopJumpCycleCommand>()
          .Has<JumpCycling>(false)
          .Add<StopWaitJumpCommand>();
      }
    }
  }
}