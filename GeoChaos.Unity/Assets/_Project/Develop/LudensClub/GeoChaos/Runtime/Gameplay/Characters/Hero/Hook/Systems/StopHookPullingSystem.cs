using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Environment.Ring;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Hook
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
        .Filter<HeroTag>()
        .Inc<StopHookPullingCommand>()
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
          .Add<StopHookCommand>()
          .Add<OnHookPullingFinished>();

        _message.CreateEntity()
          .Add<ReleaseRingMessage>();
      }
    }
  }
}