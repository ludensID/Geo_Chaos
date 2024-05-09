using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public class SpeedForceFactory : ISpeedForceFactory
  {
    private readonly PhysicsWorldWrapper _physicsWorldWrapper;

    public SpeedForceFactory(PhysicsWorldWrapper physicsWorldWrapper)
    {
      _physicsWorldWrapper = physicsWorldWrapper;
    }

    public EcsEntity Create(SpeedForceData data)
    {
      EcsEntity force = _physicsWorldWrapper.World.CreateEntity()
        .Add<SpeedForceCommand>()
        .Add((ref SpeedForce speedForce) => speedForce.Type = data.SpeedType)
        .Add((ref Owner owner) => owner.Entity = data.Owner)
        .Add((ref Impact impact) => impact.Vector = data.Impact)
        .Add((ref MovementVector vector) =>
        {
          vector.Speed = data.Speed;
          vector.Direction = data.Direction;
        })
        .Has<Instant>(data.Instant)
        .Has<Added>(data.Added)
        .Has<Unique>(data.Unique)
        .Has<Immutable>(data.Immutable)
        .Has<Draggable>(data.Draggable)
        .Has<Valuable>(data.Valuable);

      if (data.Accelerated)
      {
        force
          .Add((ref Acceleration acceleration) => acceleration.Value = data.Acceleration)
          .Add((ref MaxSpeed speed) => speed.Speed = data.MaxSpeed);
      }

      return force;
    }
  }
}