using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Move;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Move
{
  public class InterruptMovementSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _movings;
    private readonly SpeedForceLoop _forceLoop;

    public InterruptMovementSystem(GameWorldWrapper gameWorldWrapper, ISpeedForceLoopService forceLoopSvc)
    {
      _game = gameWorldWrapper.World;
      _forceLoop = forceLoopSvc.CreateLoop();

      _movings = _game
        .Filter<HeroTag>()
        .Inc<Moving>()
        .Exc<FreeFalling>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity moving in _movings
        .Check<MovementLayout>(x => x.Layer != MovementLayer.All))
      {
        moving.Del<Moving>();
        _forceLoop.ResetForcesToZero(SpeedForceType.Move, moving.PackedEntity);
      }
    }
  }
}