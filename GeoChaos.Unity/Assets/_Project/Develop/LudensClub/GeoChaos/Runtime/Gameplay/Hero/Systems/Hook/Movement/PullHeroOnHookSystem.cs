using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.Ring;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Hook
{
  public class PullHeroOnHookSystem : IEcsRunSystem
  {
    private readonly ISpeedForceFactory _forceFactory;
    private readonly IDragForceService _dragForceSvc;
    private readonly ITimerFactory _timers;
    private readonly EcsWorld _game;
    private readonly EcsEntities _finishedPrecasts;
    private readonly EcsEntities _hookedRings;
    private readonly HeroConfig _config;

    public PullHeroOnHookSystem(GameWorldWrapper gameWorldWrapper,
      ISpeedForceFactory forceFactory,
      IDragForceService dragForceSvc,
      IConfigProvider configProvider,
      ITimerFactory timers)
    {
      _forceFactory = forceFactory;
      _dragForceSvc = dragForceSvc;
      _timers = timers;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _finishedPrecasts = _game
        .Filter<OnHookPrecastFinished>()
        .Inc<ViewRef>()
        .Inc<MovementVector>()
        .Inc<GravityScale>()
        .Collect();

      _hookedRings = _game
        .Filter<RingTag>()
        .Inc<Hooked>()
        .Inc<ViewRef>()
        .Inc<RingPoints>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity ring in _hookedRings)
      foreach (EcsEntity precast in _finishedPrecasts)
      {
        Transform heroTransform = precast.Get<ViewRef>().View.transform;
        Transform targetTransform = ring.Get<RingPoints>().TargetPoint;
        Vector3 heroPosition = heroTransform.position;
        Vector3 target = targetTransform.position;

        Vector3 vector = target - heroPosition;
        float time = vector.magnitude / _config.HookVelocity;
        Vector2 velocity = vector / time;

        (Vector3 length, Vector3 direction) = MiscUtils.DecomposeVector(velocity);
        _forceFactory.Create(new SpeedForceData(SpeedForceType.Hook, precast.Pack(), Vector2.one)
        {
          Speed = length,
          Direction = direction,
          Draggable = true
        });

        precast.Add((ref HookTimer timer) => timer.TimeLeft = _timers.Create(time + _config.PullTimeOffset));

        precast
          .Has<Controlling>(false)
          .Add<OnHookPullingStarted>()
          .Add((ref HookPulling pulling) =>
          {
            pulling.Velocity = velocity;
            pulling.Target = target;
          })
          .Replace((ref GravityScale gravity) => gravity.Enabled = false);

        if (precast.Has<DragForceAvailable>())
        {
          float controlTime = time * 2 * (1 - _config.StartDragForceCoefficient * 2);
          controlTime = MathUtils.Clamp(controlTime, 0.0001f);

          EcsEntity drag = _dragForceSvc.GetDragForce(precast.Pack());
          if (_config.UseGradient)
          {
            drag.Add((ref DragForceDelay delay) =>
              delay.TimeLeft = _timers.Create(time * 2 * _config.StartDragForceCoefficient));
          }

          drag
            .Replace((ref GradientRate rate) => rate.Rate = 1 / controlTime)
            .Replace((ref RelativeSpeed relative) =>
              relative.Speed = new Vector2(Mathf.Abs(velocity.x), Mathf.Abs(velocity.y)));
        }
      }
    }
  }
}