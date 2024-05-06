using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public class ConvertToSpeedForceSystem : IEcsRunSystem
  {
    private readonly EcsWorld _physics;
    private readonly EcsEntities _commands;
    private readonly EcsEntities _forces;

    public ConvertToSpeedForceSystem(PhysicsWorldWrapper physicsWorldWrapper)
    {
      _physics = physicsWorldWrapper.World;

      _commands = _physics
        .Filter<SpeedForceCommand>()
        .Collect();

      _forces = _physics
        .Filter<SpeedForce>()
        .Exc<SpeedForceCommand>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity command in _commands)
      {
        foreach (EcsEntity force in _forces
          .Where<SpeedForce>(x => x.Type == command.Get<SpeedForce>().Type))
        {
          force.Dispose();
        }

        command.Del<SpeedForceCommand>();
      }
    }
  }
}