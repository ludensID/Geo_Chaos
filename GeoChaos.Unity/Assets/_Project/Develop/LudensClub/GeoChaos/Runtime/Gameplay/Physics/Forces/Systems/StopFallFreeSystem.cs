using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Move;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public class StopFallFreeSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsWorld _physics;
    private readonly EcsEntities _stops;
    private readonly EcsEntities _freeFalls;

    public StopFallFreeSystem(GameWorldWrapper gameWorldWrapper, PhysicsWorldWrapper physicsWorldWrapper)
    {
      _game = gameWorldWrapper.World;
      _physics = physicsWorldWrapper.World;

      _stops = _game
        .Filter<StopFallFreeCommand>()
        .Collect();

      _freeFalls = _physics
        .Filter<FreeFall>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity stop in _stops)
      {
        foreach (EcsEntity freeFall in _freeFalls
          .Where<Owner>(x => x.Entity.EqualsTo(stop.Pack())))
        {
          freeFall
            .Has<Enabled>(false)
            .Has<Prepared>(false)
            .Has<Delay>(false);
        }

        stop.Has<FreeRotating>(false);

        stop.Del<StopFallFreeCommand>();
      }
    }
  }
}