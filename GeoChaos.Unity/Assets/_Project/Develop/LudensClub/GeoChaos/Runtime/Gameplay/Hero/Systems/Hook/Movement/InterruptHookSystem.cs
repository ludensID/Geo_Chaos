using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.Ring;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Hook
{
  public class InterruptHookSystem : IEcsRunSystem
  {
    private readonly ISpeedForceFactory _forceFactory;
    private readonly ITimerFactory _timers;
    private readonly IDragForceService _dragForceSvc;
    private readonly EcsWorld _game;
    private readonly EcsEntities _precastCommands;
    private readonly EcsEntities _pullCommands;
    private readonly EcsEntities _landCommands;
    private readonly EcsEntities _hookedRings;
    private readonly HeroConfig _config;
    private readonly EcsWorld _physics;
    private readonly EcsEntities _drags;

    public InterruptHookSystem(GameWorldWrapper gameWorldWrapper,
      PhysicsWorldWrapper physicsWorldWrapper,
      ISpeedForceFactory forceFactory,
      ITimerFactory timers,
      IConfigProvider configProvider,
      IDragForceService dragForceSvc)
    {
      _forceFactory = forceFactory;
      _timers = timers;
      _dragForceSvc = dragForceSvc;
      _game = gameWorldWrapper.World;
      _physics = physicsWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _precastCommands = _game
        .Filter<InterruptHookCommand>()
        .Inc<HookPrecast>()
        .Collect();

      _pullCommands = _game
        .Filter<InterruptHookCommand>()
        .Inc<HookPulling>()
        .Collect();

      _landCommands = _game
        .Filter<InterruptHookCommand>()
        .Inc<HookFalling>()
        .Collect();

      _hookedRings = _game
        .Filter<RingTag>()
        .Inc<Hooked>()
        .Collect();

      _drags = _physics
        .Filter<DragForce>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity precast in _precastCommands)
      {
        precast
          .Del<InterruptHookCommand>()
          .Del<HookPrecast>()
          .Has<OnHookPrecastStarted>(false)
          .Has<OnHookPrecastFinished>(false)
          .Add<OnHookInterrupted>();
      }

      foreach (EcsEntity pull in _pullCommands)
      {
        _forceFactory.Create(new SpeedForceData(SpeedForceType.Hook, pull.Pack()));

        pull
          .Del<InterruptHookCommand>()
          .Add<OnHookInterrupted>()
          .Del<HookPulling>()
          .Del<HookTimer>()
          .Has<OnHookPullingStarted>(false)
          .Has<OnHookPullingFinished>(false)
          .Has<Controlling>(false)
          .Replace((ref GravityScale gravity) =>
          {
            gravity.Enabled = true;
            gravity.Override = true;
          });

        foreach (EcsEntity ring in _hookedRings)
        {
          ring
            .Del<Hooked>()
            .Add((ref Releasing releasing) => releasing.TimeLeft = _timers.Create(_config.RingReleasingTime));
        }


        _dragForceSvc.GetDragForce(pull.Pack())
          .Has<Enabled>(false)
          .Has<DragForceDelay>(false);
      }

      foreach (EcsEntity land in _landCommands)
      {
        _forceFactory.Create(new SpeedForceData(SpeedForceType.Hook, land.Pack()));

        land
          .Del<InterruptHookCommand>()
          .Add<OnHookInterrupted>()
          .Del<HookFalling>()
          .Has<Controlling>(false);

        _dragForceSvc.GetDragForce(land.Pack())
          .Has<Enabled>(false)
          .Has<DragForceDelay>(false);
      }
    }
  }
}