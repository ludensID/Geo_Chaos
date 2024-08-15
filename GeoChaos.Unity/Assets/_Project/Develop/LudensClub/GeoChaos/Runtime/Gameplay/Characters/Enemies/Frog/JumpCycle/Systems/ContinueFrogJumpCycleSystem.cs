using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.JumpCycle
{
  public class ContinueFrogJumpCycleSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _cyclingFrogs;

    public ContinueFrogJumpCycleSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _cyclingFrogs = _game
        .Filter<FrogTag>()
        .Inc<ContinueJumpCycleCommand>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity frog in _cyclingFrogs)
      {
        frog.Del<ContinueJumpCycleCommand>();

        if (frog.Has<JumpCyclePausing>())
        {
          frog
            .Del<JumpCyclePausing>()
            .Add<JumpCycling>()
            .Has<PrepareJumpCommand>(true);
        }
      }
    }
  }
}