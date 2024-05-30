using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Move;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public class StartFallFreeSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsWorld _physics;
    private readonly EcsEntities _falls;
    private readonly EcsEntities _prepares;

    public StartFallFreeSystem(GameWorldWrapper gameWorldWrapper, PhysicsWorldWrapper physicsWorldWrapper)
    {
      _game = gameWorldWrapper.World;
      _physics = physicsWorldWrapper.World;

      _falls = _game
        .Filter<FallFreeCommand>()
        .Collect();

      _prepares = _physics
        .Filter<Prepared>()
        .Exc<Delay>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity fall in _falls)
      {
        foreach (EcsEntity prepare in _prepares
          .Where<Owner>(x => x.Entity.EqualsTo(fall.Pack())))
        {
          prepare
            .Del<Prepared>()
            .Add<Enabled>()
            .Replace((ref Gradient gradient) => gradient.Value = 0);
          
          fall.Has<FreeRotating>(prepare.Has<ADControl>());
        }

        fall.Del<FallFreeCommand>();
      }
    }
  }
}