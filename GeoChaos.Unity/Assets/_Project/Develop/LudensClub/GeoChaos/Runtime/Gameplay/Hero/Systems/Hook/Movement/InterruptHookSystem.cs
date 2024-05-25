using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.Ring;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Hook
{
  public class InterruptHookSystem : IEcsRunSystem
  {
    private readonly ISpeedForceFactory _forceFactory;
    private readonly EcsWorld _game;
    private readonly EcsEntities _precastCommands;
    private readonly EcsEntities _pullCommands;
    private readonly EcsEntities _landCommands;
    private readonly EcsWorld _message;

    public InterruptHookSystem(GameWorldWrapper gameWorldWrapper,
      MessageWorldWrapper messageWorldWrapper,
      ISpeedForceFactory forceFactory)
    {
      _forceFactory = forceFactory;
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
          .Add<StopFallFreeCommand>()
          .Del<HookPulling>()
          .Del<HookTimer>()
          .Has<OnHookPullingStarted>(false)
          .Has<OnHookPullingFinished>(false)
          .Replace((ref GravityScale gravity) => gravity.Enabled = true);

        ReleaseRing();
      }

      foreach (EcsEntity land in _landCommands)
      {
        InterruptHookSpeed(land);
        land
          .Del<InterruptHookCommand>()
          .Add<OnHookInterrupted>()
          .Add<StopFallFreeCommand>()
          .Del<HookFalling>();
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