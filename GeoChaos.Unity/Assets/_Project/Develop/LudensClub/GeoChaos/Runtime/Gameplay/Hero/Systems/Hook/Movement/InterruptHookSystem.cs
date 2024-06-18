using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.Ring;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Hook
{
  public class InterruptHookSystem : IEcsRunSystem
  {
    private readonly ISpeedForceFactory _forceFactory;
    private readonly EcsWorld _game;
    private readonly EcsWorld _message;
    private readonly EcsEntities _interruptCommands;

    public InterruptHookSystem(GameWorldWrapper gameWorldWrapper,
      MessageWorldWrapper messageWorldWrapper,
      ISpeedForceFactory forceFactory)
    {
      _forceFactory = forceFactory;
      _game = gameWorldWrapper.World;
      _message = messageWorldWrapper.World;

      _interruptCommands = _game
        .Filter<InterruptHookCommand>()
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
            .Add<OnActionFinished>()
            .Del<HookPulling>()
            .Del<HookTimer>()
            .Has<OnHookPullingStarted>(false)
            .Has<OnHookPullingFinished>(false)
            .Replace((ref GravityScale gravity) => gravity.Enabled = true);
        }

        if (hasPrecast || hasPulling)
          ReleaseRing();
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
  }
}