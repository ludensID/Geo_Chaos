using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Move;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Move
{
  public class UpdateControlSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _controllings;
    private readonly SpeedForceLoop _forceLoop;
    private readonly HeroConfig _config;

    public UpdateControlSystem(GameWorldWrapper gameWorldWrapper, ISpeedForceLoopService forceLoopSvc, IConfigProvider configProvider)
    {
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _forceLoop = forceLoopSvc.CreateLoop();

      _controllings = _game
        .Filter<Controlling>()
        .Inc<Moving>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity controlling in _controllings)
      {
        ref Controlling controlData = ref controlling.Get<Controlling>();
        float factor = controlling.Get<ControlFactor>().Factor;
        float m = controlData.MaxSpeed;
        float a = controlData.Acceleration;
        foreach(EcsEntity force in _forceLoop
          .GetLoop(SpeedForceType.Move, controlling.Pack()))
        {
          float mf = force.Get<MaxSpeed>().Speed;
          force
            .Replace((ref MovementVector vector) => vector.Speed.x *= mf != 0 ? m / mf * factor * _config.SpeedRatio : 0)
            .Replace((ref Acceleration acceleration) => acceleration.Value.x = a * factor * _config.SpeedRatio)
            .Replace((ref MaxSpeed speed) => speed.Speed = m * factor * _config.SpeedRatio);
        }
      }
    }
  }
}