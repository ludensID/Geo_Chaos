using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.JumpCycle
{
  public class PauseFrogJumpCycleSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _cyclingFrogs;

    public PauseFrogJumpCycleSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _cyclingFrogs = _game
        .Filter<FrogTag>()
        .Inc<PauseJumpCycleCommand>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity frog in _cyclingFrogs)
      {
        frog
          .Del<PauseJumpCycleCommand>()
          .Has<JumpCyclePausing>(frog.Has<JumpCycling>())
          .Has<PrepareJumpCommand>(false)
          .Has<JumpCycling>(false);
      }
    }
  }
}