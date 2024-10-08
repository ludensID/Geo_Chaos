﻿using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public class PrepareDragForceSystem : IEcsRunSystem
  {
    private readonly IFreeFallService _freeFallSvc;
    private readonly EcsWorld _game;
    private readonly EcsWorld _physics;
    private readonly HeroConfig _config;
    private readonly EcsEntities _actionEvents;
    private readonly EcsEntities _dragForces;

    public PrepareDragForceSystem(GameWorldWrapper gameWorldWrapper,
      PhysicsWorldWrapper physicsWorldWrapper,
      IConfigProvider configProvider,
      IFreeFallService freeFallSvc)
    {
      _freeFallSvc = freeFallSvc;
      _game = gameWorldWrapper.World;
      _physics = physicsWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _actionEvents = _game
        .Filter<OnActionStarted>()
        .Inc<DragForceAvailable>()
        .Collect();

      _dragForces = _physics
        .Filter<DragForce>()
        .Inc<PrepareCommand>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity action in _actionEvents)
      {
        ref ActionContext ctx = ref action.Get<ActionContext>();

        foreach (EcsEntity drag in _dragForces
          .Check<Owner>(owner => owner.Entity.EqualsTo(action.PackedEntity)))
        {
          _freeFallSvc.PrepareFreeFall(drag, ctx.Time, _config.StartDragForceCoefficient,
            _config.UseDragForceGradient);

          Vector2 velocity = ctx.Velocity;
          drag.Change((ref RelativeSpeed relative) =>
              relative.Speed = new Vector2(Mathf.Abs(velocity.x), Mathf.Abs(velocity.y)));
        }
      }
    }
  }
}