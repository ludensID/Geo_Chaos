using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Jump;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.JumpWait;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Jump;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.JumpCycle
{
  public class StopFrogJumpCycleSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _cyclingFrogs;

    public StopFrogJumpCycleSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _cyclingFrogs = _game
        .Filter<FrogTag>()
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
          .Has<JumpCyclePausing>(false)
          .Has<JumpPoint>(false)
          .Has<FrogJumpContext>(false)
          .Has<JumpCommand>(false)
          .Add<StopJumpCommand>()
          .Add<StopWaitJumpCommand>();
      }
    }
  }
}