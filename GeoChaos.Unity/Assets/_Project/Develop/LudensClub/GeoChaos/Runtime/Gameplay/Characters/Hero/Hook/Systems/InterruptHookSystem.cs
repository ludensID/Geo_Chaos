using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Immunity;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Damage;
using LudensClub.GeoChaos.Runtime.Gameplay.Environment.Ring;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Gravity;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Hook
{
  public class InterruptHookSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsWorld _message;
    private readonly EcsEntities _interruptCommands;
    private readonly HeroConfig _config;
    private readonly SpeedForceLoop _forceLoop;

    public InterruptHookSystem(GameWorldWrapper gameWorldWrapper,
      MessageWorldWrapper messageWorldWrapper,
      IConfigProvider configProvider,
      ISpeedForceLoopService forceLoopSvc)
    {
      _game = gameWorldWrapper.World;
      _message = messageWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();
      _forceLoop = forceLoopSvc.CreateLoop();

      _interruptCommands = _game
        .Filter<HeroTag>()
        .Inc<InterruptHookCommand>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity interrupt in _interruptCommands)
      {
        interrupt
          .Del<InterruptHookCommand>()
          .Add<OnHookInterrupted>();

        bool hasPrecast = interrupt.Has<HookPrecast>();
        bool hasPulling = interrupt.Has<HookPulling>();

        if (hasPulling)
          InterruptHookSpeed(interrupt);

        if (hasPrecast)
        {
          interrupt.Del<HookPrecast>()
            .Has<OnHookPrecastStarted>(false)
            .Has<OnHookPrecastFinished>(false);
        }
        else if (hasPulling)
        {
          interrupt
            .Replace((ref ActionState actionState) => actionState.States.Add(StateType.Finish))
            .Del<HookPulling>()
            .Del<HookTimer>()
            .Has<OnHookPullingStarted>(false)
            .Has<OnHookPullingFinished>(false)
            .Change((ref GravityScale gravity) => gravity.Enabled = true);
        }

        if (_config.BumpOnHookReaction == BumpOnHookReactionType.Immunity && interrupt.Has<Immune>()
          && interrupt.Get<Immune>().Owner == MovementType.Hook)
        {
          interrupt
            .Del<Immune>()
            .Add<OnImmunityFinished>();
        }

        if (hasPrecast || hasPulling)
          ReleaseRing();
      }
    }

    private void InterruptHookSpeed(EcsEntity entity)
    {
      _forceLoop.ResetForcesToZero(SpeedForceType.Hook, entity.PackedEntity);
    }

    private void ReleaseRing()
    {
      _message.CreateEntity()
        .Add<ReleaseRingMessage>();
    }
  }
}