using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Lock;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Move
{
  public class SowMoveCommandSystem : IEcsRunSystem
  {
    private readonly IADControlService _controlSvc;
    private readonly EcsWorld _game;
    private readonly EcsEntities _commands;

    public SowMoveCommandSystem(GameWorldWrapper gameWorldWrapper, IADControlService controlSvc)
    {
      _controlSvc = controlSvc;
      _game = gameWorldWrapper.World;

      _commands = _game
        .Filter<MoveCommand>()
        .Inc<MovementLocked>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity command in _commands)
      {
        EcsEntity control = _controlSvc.GetADControl(command.Pack());
        if (control == null || !control.Has<Enabled>())
          command.Del<MoveCommand>();
      }
    }
  }
}