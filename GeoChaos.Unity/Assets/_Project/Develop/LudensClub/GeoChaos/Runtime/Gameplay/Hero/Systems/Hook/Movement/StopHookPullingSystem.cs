using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Ring;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Hook
{
  public class StopHookPullingSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _stopCommands;
    private readonly EcsWorld _message;

    public StopHookPullingSystem(GameWorldWrapper gameWorldWrapper, MessageWorldWrapper messageWorldWrapper)
    {
      _game = gameWorldWrapper.World;
      _message = messageWorldWrapper.World;

      _stopCommands = _game
        .Filter<StopHookPullingCommand>()
        .Inc<HookPulling>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity command in _stopCommands)
      {
        command
          .Del<HookPulling>()
          .Del<HookTimer>()
          .Del<StopHookPullingCommand>()
          .Add<OnHookPullingFinished>();

        _message.CreateEntity()
          .Add<ReleaseRingMessage>();
      }
    }
  }
}