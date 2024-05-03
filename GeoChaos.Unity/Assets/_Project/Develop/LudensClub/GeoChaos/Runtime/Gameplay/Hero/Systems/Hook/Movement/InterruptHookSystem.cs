using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
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

    public InterruptHookSystem(GameWorldWrapper gameWorldWrapper, ISpeedForceFactory forceFactory)
    {
      _forceFactory = forceFactory;
      _game = gameWorldWrapper.World;

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
          .Del<HookPrecast>()
          .Is<OnHookPrecastStarted>(false)
          .Is<OnHookPrecastFinished>(false)
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
          .Is<ControlDelay>(false)
          .Is<OnHookPullingStarted>(false)
          .Is<OnHookPullingFinished>(false)
          .Is<DragForcing>(false)
          .Is<Controlling>(false);
      }

      foreach (EcsEntity land in _landCommands)
      {
        land
          .Del<InterruptHookCommand>()
          .Add<OnHookInterrupted>()
          .Del<HookFalling>()
          .Is<DragForcing>(false)
          .Is<Controlling>(false);
      }
    }
  }
}