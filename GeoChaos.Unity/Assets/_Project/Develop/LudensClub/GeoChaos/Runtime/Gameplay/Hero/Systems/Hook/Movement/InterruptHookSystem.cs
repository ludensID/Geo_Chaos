using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Hook
{
  public class InterruptHookSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _precastCommands;
    private readonly EcsEntities _pullCommands;
    private readonly SpeedForceLoop _forceLoop;

    public InterruptHookSystem(GameWorldWrapper gameWorldWrapper, ISpeedForceLoopService forceLoopSvc)
    {
      _game = gameWorldWrapper.World;

      _forceLoop = forceLoopSvc.CreateLoop();

      _precastCommands = _game
        .Filter<InterruptHookCommand>()
        .Inc<HookPrecast>()
        .Collect();

      _pullCommands = _game
        .Filter<InterruptHookCommand>()
        .Inc<HookPulling>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity command in _precastCommands)
      {
        command
          .Del<InterruptHookCommand>()
          .Del<HookPrecast>()
          .DelEnsure<OnHookPrecastStarted>()
          .DelEnsure<OnHookPrecastFinished>()
          .Add<OnHookInterrupted>();
      }

      foreach (EcsEntity command in _pullCommands)
      {
        foreach (EcsEntity force in _forceLoop
          .GetLoop(SpeedForceType.Hook, command.Pack()))
        {
          force
            .Del<Unique>()
            .Del<Immutable>()
            .Add<Instant>();
        }

        command
          .Del<InterruptHookCommand>()
          .Add<OnHookInterrupted>()
          .Del<HookPulling>()
          .Del<HookTimer>()
          .DelEnsure<StopHookPullingCommand>()
          .DelEnsure<OnHookPullingStarted>()
          .DelEnsure<OnHookPullingFinished>();
      }
    }
  }
}