using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public class IgnoreMoveForcesSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _freeFalls;
    private readonly SpeedForceLoop _forceLoop;

    public IgnoreMoveForcesSystem(GameWorldWrapper gameWorldWrapper, ISpeedForceLoopService forceLoopSvc)
    {
      _game = gameWorldWrapper.World;

      _forceLoop = forceLoopSvc.CreateLoop();

      _freeFalls = _game
        .Filter<FreeFalling>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity fall in _freeFalls)
      {
        foreach (EcsEntity move in _forceLoop
          .GetLoop(SpeedForceType.Move, fall.PackedEntity))
        {
          move.Has<Ignored>(true);
        }
      }
    }
  }
}