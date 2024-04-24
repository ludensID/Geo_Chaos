using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Hook
{
  public class ConvertDelayedToCurrentHookInputSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _delayedCommands;

    public ConvertDelayedToCurrentHookInputSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _delayedCommands = _game
        .Filter<DelayHookCommand>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity command in _delayedCommands)
      {
        command
          .Del<DelayHookCommand>()
          .Add<HookCommand>();
      }
    }
  }
}