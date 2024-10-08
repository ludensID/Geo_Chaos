﻿using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public class DiscardADControlSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _vectors;
    private readonly EcsWorld _physics;
    private readonly EcsEntities _controls;
    private readonly BelongOwnerClosure _belongOwnerClosure = new BelongOwnerClosure();

    public DiscardADControlSystem(GameWorldWrapper gameWorldWrapper,
      PhysicsWorldWrapper physicsWorldWrapper)
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
        foreach (EcsEntity control in _controls
          .Check(_belongOwnerClosure.SpecifyPredicate(vector.PackedEntity)))
        {
          float speed = control.Get<ControlSpeed>().Speed;
          movementVector.Speed.x = MathUtils.DecreaseToZero(movementVector.Speed.x, Mathf.Abs(speed));
        }
      }
    }
  }
}