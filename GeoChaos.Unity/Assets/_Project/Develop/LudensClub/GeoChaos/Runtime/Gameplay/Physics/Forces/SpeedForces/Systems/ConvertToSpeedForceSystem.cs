using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public class ConvertToSpeedForceSystem : IEcsRunSystem
  {
    private readonly EcsWorld _physics;
    private readonly EcsEntities _commands;
    private readonly SpeedForceLoop _forces;

    public ConvertToSpeedForceSystem(PhysicsWorldWrapper physicsWorldWrapper, ISpeedForceLoopService forceLoopSvc)
    {
      _physics = physicsWorldWrapper.World;

      _commands = _physics
        .Filter<SpeedForceCommand>()
        .Collect();

      _forces = forceLoopSvc.CreateLoop(x => x.Exc<SpeedForceCommand>());
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity command in _commands)
      {
        foreach (EcsEntity force in _forces
          .GetLoop(command.Get<SpeedForce>().Type, command.Get<Owner>().Entity))
        {
          force.Dispose();
        }

        command.Del<SpeedForceCommand>();
      }
    }
  }
}