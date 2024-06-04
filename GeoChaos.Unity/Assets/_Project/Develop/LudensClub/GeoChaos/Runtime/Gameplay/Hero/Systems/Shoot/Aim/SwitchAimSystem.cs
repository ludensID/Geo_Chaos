using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Lock;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Shoot.Aim;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Shoot.Aim
{
  public class SwitchAimSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _startCommands;
    private readonly EcsEntities _finishCommands;

    public SwitchAimSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _startCommands = _game
        .Filter<StartAimCommand>()
        .Collect();

      _finishCommands = _game
        .Filter<FinishAimCommand>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity command in _startCommands)
      {
        if (!command.Has<MovementLocked>())
        {
          command
            .Add<OnAimStarted>()
            .Add<Aiming>();
        }

        command.Del<StartAimCommand>();
      }

      foreach (EcsEntity command in _finishCommands)
      {
        command
          .Add<OnAimFinished>()
          .Del<Aiming>()
          .Del<FinishAimCommand>();
      }
    }
  }
}