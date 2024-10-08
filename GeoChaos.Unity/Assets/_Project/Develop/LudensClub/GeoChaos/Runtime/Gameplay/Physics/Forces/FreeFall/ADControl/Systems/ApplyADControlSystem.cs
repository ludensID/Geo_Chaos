﻿using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public class ApplyADControlSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsWorld _physics;
    private readonly EcsEntities _vectors;
    private readonly EcsEntities _controls;

    public ApplyADControlSystem(GameWorldWrapper gameWorldWrapper, PhysicsWorldWrapper physicsWorldWrapper)
    {
      _game = gameWorldWrapper.World;
      _physics = physicsWorldWrapper.World;

      _vectors = _game
        .Filter<MovementVector>()
        .Inc<ADControllable>()
        .Collect();

      _controls = _physics
        .Filter<ADControl>()
        .Inc<Enabled>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity vector in _vectors)
      {
        ref MovementVector movementVector = ref vector.Get<MovementVector>();
        Vector2 velocity = movementVector.Speed * movementVector.Direction;
        foreach (EcsEntity control in _controls
          .Check<Owner>(x => x.Entity.EqualsTo(vector.PackedEntity)))
        {
          float speed = control.Get<ControlSpeed>().Speed;
          velocity.x += speed;
          movementVector.AssignVector(velocity, true);
        }
      }
    }
  }
}