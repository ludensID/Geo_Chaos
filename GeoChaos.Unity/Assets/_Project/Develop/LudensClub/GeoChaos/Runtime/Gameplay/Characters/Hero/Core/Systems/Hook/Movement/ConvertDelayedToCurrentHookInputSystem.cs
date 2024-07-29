using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Systems.Hook
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