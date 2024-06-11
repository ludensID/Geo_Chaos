using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
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
        .Collect()
        .Where<SpeedForce>(x => x.Type == SpeedForceType.Move);
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity move in _moves)
      {
        if (move.Get<Owner>().Entity.TryUnpackEntity(_game, out EcsEntity owner) && !owner.Has<FreeFalling>())
          move.Del<Ignored>();
      }
    }
  }
}