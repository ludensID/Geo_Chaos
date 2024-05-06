using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Move;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Move
{
  public class TurnOnADControlSystem : IEcsRunSystem
  {
    private readonly ISpeedForceFactory _forceFactory;
    private readonly EcsWorld _game;
    private readonly EcsEntities _controllings;
    private readonly SpeedForceLoop _forceLoop;

    public TurnOnADControlSystem(GameWorldWrapper gameWorldWrapper,
      ISpeedForceLoopService forceLoopSvc,
      ISpeedForceFactory forceFactory)
    {
      _forceFactory = forceFactory;
      _game = gameWorldWrapper.World;

      _forceLoop = forceLoopSvc.CreateLoop();

      _controllings = _game
        .Filter<Controllable>()
        .Inc<Controlling>()
        .Inc<Moving>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity controlling in _controllings)
      {
        float factor = controlling.Get<ControlFactor>().Factor;
        foreach (EcsEntity force in _forceLoop
          .GetLoop(SpeedForceType.Move, controlling.Pack()))
        {
          controlling.Replace((ref Controlling control) =>
          {
            control.MaxSpeed = force.Get<MaxSpeed>().Speed;
            control.Acceleration = force.Get<Acceleration>().Value.x;
          });

          force
            .Has<Added>(true)
            .Replace((ref Acceleration acceleration) => acceleration.Value.x *= factor)
            .Replace((ref MaxSpeed speed) => speed.Speed *= factor);
        }
      }
    }
  }
}