using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Hook
{
  public class StopHookPullingSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _stopCommands;

    public StopHookPullingSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

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
          .Del<StopHookPullingCommand>()
          .Add<OnHookPullingFinished>();
      }
    }
  }
}