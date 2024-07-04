using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
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
        ref OnActionStarted startedAction = ref action.Get<OnActionStarted>();

        foreach (EcsEntity drag in _dragForces
          .Where<Owner>(owner => owner.Entity.EqualsTo(action.Pack())))
        {
          _freeFallSvc.PrepareFreeFall(drag, startedAction.Time, _config.StartDragForceCoefficient,
            _config.UseDragForceGradient);

          Vector2 velocity = startedAction.Velocity;
          drag.Change((ref RelativeSpeed relative) =>
              relative.Speed = new Vector2(Mathf.Abs(velocity.x), Mathf.Abs(velocity.y)));
        }
      }
    }
  }
}