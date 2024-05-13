using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Hook
{
  public class CheckForHeroReachRingSystem : IEcsRunSystem
  {
    private readonly ISpeedForceFactory _forceFactory;
    private readonly IDragForceService _dragForceSvc;
    private readonly EcsWorld _game;
    private readonly EcsEntities _pullings;
    private readonly HeroConfig _config;

    public CheckForHeroReachRingSystem(GameWorldWrapper gameWorldWrapper,
      ISpeedForceFactory forceFactory,
      IConfigProvider configProvider,
      IDragForceService dragForceSvc)
    {
      _forceFactory = forceFactory;
      _dragForceSvc = dragForceSvc;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();


      _pullings = _game
        .Filter<HookPulling>()
        .Inc<ViewRef>()
        .Inc<MovementVector>()
        .Exc<StopHookPullingCommand>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity pulling in _pullings)
      {
        Transform heroTransform = pulling.Get<ViewRef>().View.transform;
        ref HookPulling hookPulling = ref pulling.Get<HookPulling>();
        if (IsHeroReachedRing(hookPulling.Target, heroTransform.position, hookPulling.Velocity))
        {
          _forceFactory.Create(new SpeedForceData(SpeedForceType.Hook, pulling.Pack()));
          if (pulling.Has<DragForceAvailable>() && !_config.UseGradient)
          {
            _dragForceSvc.GetDragForce(pulling.Pack())
              .Add<Enabled>();
          }

          pulling
            .Add<StopHookPullingCommand>()
            .Replace((ref GravityScale gravity) => gravity.Enabled = true);
        }
      }
    }

    private static bool IsHeroReachedRing(Vector2 ring, Vector2 hero, Vector2 velocity)
    {
      Vector2 distance = ring - hero;
      Vector2 delta = distance * velocity;
      return delta.x <= 0 || delta.y <= 0 || distance.sqrMagnitude <= 0.1;
    }
  }
}