using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using NSubstitute;
using UnityEngine;

namespace LudensClub.GeoChaos.Testing
{
  public static class Create
  {
    public static IConfigProvider ConfigProvider()
    {
      return Substitute.For<IConfigProvider>();
    }

    public static ITimerService TimerService()
    {
      return Substitute.For<ITimerService>();
    }

    public static int Hero(EcsWorld world)
    {
      var hero = world.NewEntity();
      world.Add<HeroTag>(hero);
      return hero;
    }

    public static EcsEntity SpeedForce(PhysicsWorldWrapper physics,
      SpeedForceType type = SpeedForceType.Move,
      Vector2 speed = default(Vector2),
      Vector2 direction = default(Vector2),
      EcsPackedEntity owner = new EcsPackedEntity(), 
      Impact impact = default(Impact))
    {
      return physics.World.CreateEntity()
        .Add((ref SpeedForce speedForce) => speedForce.Type = type)
        .Add((ref MovementVector vector) =>
        {
          vector.Speed = speed;
          vector.Direction = direction;
        })
        .Add((ref Owner o) => o.Entity = owner)
        .Add((ref Impact i) => i = impact);
    }
  }
}