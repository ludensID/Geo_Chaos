using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.JumpCycle
{
  public class StartFrogJumpCycleSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _startCyclingFrogs;

    public StartFrogJumpCycleSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;
      
      _startCyclingFrogs = _game
        .Filter<FrogTag>()
        .Inc<StartJumpCycleCommand>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity frog in _startCyclingFrogs)
      {
        frog.Del<StartJumpCycleCommand>()
          .Add<JumpCycling>()
          .Add<PrepareJumpCommand>();
      }
    }
  }
}