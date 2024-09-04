using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public class StopIgnoreMoveForcesSystem : IEcsRunSystem
  {
    private readonly EcsWorld _physics;
    private readonly EcsEntities _moves;
    private readonly EcsWorld _game;

    public StopIgnoreMoveForcesSystem(PhysicsWorldWrapper physicsWorldWrapper, GameWorldWrapper gameWorldWrapper)
    {
      _physics = physicsWorldWrapper.World;
      _game = gameWorldWrapper.World;

      _moves = _physics
        .Filter<SpeedForce>()
        .Inc<Ignored>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity move in _moves
        .Check<SpeedForce>(x => x.Type == SpeedForceType.Move))
      {
        if (move.Get<Owner>().Entity.TryUnpackEntity(_game, out EcsEntity owner) 
          && !owner.Has<FreeFalling>())
          move.Del<Ignored>();
      }
    }
  }
}