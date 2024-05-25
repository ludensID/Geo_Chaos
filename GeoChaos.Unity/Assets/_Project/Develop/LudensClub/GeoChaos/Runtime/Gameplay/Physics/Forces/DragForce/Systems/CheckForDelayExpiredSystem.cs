using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Move;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public class CheckForDelayExpiredSystem : IEcsRunSystem
  {
    private readonly EcsWorld _physics;
    private readonly EcsWorld _game;
    private readonly EcsEntities _delays;

    public CheckForDelayExpiredSystem(PhysicsWorldWrapper physicsWorldWrapper, GameWorldWrapper gameWorldWrapper)
    {
      _physics = physicsWorldWrapper.World;
      _game = gameWorldWrapper.World;

      _delays = _physics
        .Filter<Delay>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity delay in _delays
        .Where<Delay>(x => x.TimeLeft <= 0))
      {
        delay
          .Del<Delay>()
          .Del<Prepared>()
          .Add<Enabled>()
          .Replace((ref Gradient gradient) => gradient.Value = 0);

        _game.UnpackEntity(delay.Get<Owner>().Entity)
          .Has<FreeRotating>(delay.Has<ADControl>());
      }
    }
  }
}