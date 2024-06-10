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
    private readonly ITimerFactory _timers;
    private readonly EcsWorld _game;
    private readonly EcsEntities _finishedPrecasts;
    private readonly EcsEntities _hookedRings;
    private readonly HeroConfig _config;

    public PullHeroOnHookSystem(GameWorldWrapper gameWorldWrapper,
      ISpeedForceFactory forceFactory,
      IConfigProvider configProvider,
      ITimerFactory timers)
    {
      _forceFactory = forceFactory;
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
        .Inc<RingPoints>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity ring in _hookedRings)
      foreach (EcsEntity precast in _finishedPrecasts)
      {
        Vector3 precastPosition = precast.Get<ViewRef>().View.transform.position;
        Vector3 target = ring.Get<RingPoints>().TargetPoint.position;

        Vector3 vector = target - precastPosition;
        float time = vector.magnitude / _config.HookVelocity;
        Vector2 velocity = vector / time;

        (Vector3 length, Vector3 direction) = MathUtils.DecomposeVector(velocity);
        _forceFactory.Create(new SpeedForceData(SpeedForceType.Hook, precast.Pack(), Vector2.one)
        {
          Speed = length,
          Direction = direction,
          Draggable = true
        });

        precast
          .Add((ref HookTimer timer) => timer.TimeLeft = _timers.Create(time + _config.PullTimeOffset))
          .Add<OnHookPullingStarted>()
          .Add((ref HookPulling pulling) =>
          {
            pulling.Velocity = velocity;
            pulling.Target = target;
          })
          .Replace((ref GravityScale gravity) => gravity.Enabled = false)
          .Add((ref OnActionStarted action) =>
          {
            action.Time = time;
            action.Velocity = velocity;
          });
      }
    }
  }
}