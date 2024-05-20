using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Move;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.Ring;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Hook
{
  public class InterruptHookSystem : IEcsRunSystem
  {
    private readonly ISpeedForceFactory _forceFactory;
    private readonly IDragForceService _dragForceSvc;
    private readonly IADControlService _controlSvc;
    private readonly EcsWorld _game;
    private readonly EcsEntities _precastCommands;
    private readonly EcsEntities _pullCommands;
    private readonly EcsEntities _landCommands;
    private readonly EcsWorld _message;

    public InterruptHookSystem(GameWorldWrapper gameWorldWrapper,
      MessageWorldWrapper messageWorldWrapper,
      ISpeedForceFactory forceFactory,
      IDragForceService dragForceSvc,
      IADControlService controlSvc)
    {
      _forceFactory = forceFactory;
      _dragForceSvc = dragForceSvc;
      _controlSvc = controlSvc;
      _game = gameWorldWrapper.World;
      _message = messageWorldWrapper.World;

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
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity precast in _precastCommands)
      {
        precast
          .Del<InterruptHookCommand>()
          .Add<OnHookInterrupted>()
          .Del<HookPrecast>()
          .Has<OnHookPrecastStarted>(false)
          .Has<OnHookPrecastFinished>(false);
        
        ReleaseRing();
      }

      foreach (EcsEntity pull in _pullCommands)
      {
        InterruptHookSpeed(pull);

        pull
          .Del<InterruptHookCommand>()
          .Add<OnHookInterrupted>()
          .Del<HookPulling>()
          .Del<HookTimer>()
          .Has<OnHookPullingStarted>(false)
          .Has<OnHookPullingFinished>(false)
          .Replace((ref GravityScale gravity) => gravity.Enabled = true);

        ReleaseRing();

        InterruptFallUpgrades(pull);
      }

      foreach (EcsEntity land in _landCommands)
      {
        InterruptHookSpeed(land);

        land
          .Del<InterruptHookCommand>()
          .Add<OnHookInterrupted>()
          .Del<HookFalling>();

        InterruptFallUpgrades(land);
      }
    }

    private void InterruptHookSpeed(EcsEntity entity)
    {
      _forceFactory.Create(new SpeedForceData(SpeedForceType.Hook, entity.Pack()));
    }

    private void ReleaseRing()
    {
      _message.CreateEntity()
        .Add<ReleaseRingMessage>();
    }

    private void InterruptFallUpgrades(EcsEntity owner)
    {
      foreach (EcsEntity drag in _dragForceSvc.GetLoop(owner.Pack()))
      {
        drag
          .Has<Enabled>(false)
          .Has<Delay>(false);
      }

      foreach (EcsEntity control in _controlSvc.GetLoop(owner.Pack()))
      {
        owner.Has<FreeRotating>(false);
        
        control
          .Has<Enabled>(false)
          .Has<Prepared>(false)
          .Has<Delay>(false);
      }
    }
  }
}