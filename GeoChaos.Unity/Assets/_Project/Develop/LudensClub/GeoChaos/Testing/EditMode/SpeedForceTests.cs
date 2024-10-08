﻿using FluentAssertions;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using NUnit.Framework;
using UnityEngine;

namespace LudensClub.GeoChaos.Testing.EditMode
{
  public class SpeedForceTests
  {
    [Test]
    public void WhenCalculateTargetVelocityAndOneSimpleSpeedForceThenVectorSpeedShouldBeEqualToSimpleSpeed()
    {
      // Arrange.
      var speed = new Vector2(20, 30);
      var systems = new EcsSystems(new EcsWorld());
      var game = new GameWorldWrapper();
      var physics = new PhysicsWorldWrapper();
      EcsEntity entity = game.World.CreateEntity();
      entity.Add<MovementVector>()
        .Add<ForceAvailable>();
      Create.SpeedForce(physics, speed: speed, direction: Vector2.one, impact: new Impact { Vector = Vector2.one },
        owner: entity.PackedEntity);

      ref MovementVector vector = ref entity.Get<MovementVector>();
      systems.Add(new CalculateTargetMovementVectorSystem(physics, game));
      systems.Init();

      // Act.
      systems.Run();

      // Assert.
      vector.Speed.Should().Be(speed);
    }
  }
}