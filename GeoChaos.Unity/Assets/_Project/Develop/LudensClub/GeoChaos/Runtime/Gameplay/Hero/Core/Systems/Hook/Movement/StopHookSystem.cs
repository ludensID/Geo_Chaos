using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Hook
{
  public class StopHookSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _commands;
    private readonly EcsEntities _selectedRings;

    public StopHookSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _commands = _game
        .Filter<StopHookCommand>()
        .Inc<Hooking>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity command in _commands)
      {
        command
          .Del<StopHookCommand>()
          .Del<Hooking>();
        
        ref MovementLayout layout = ref command.Get<MovementLayout>();
        if (layout.Owner == MovementType.Hook)
        {
          layout.Layer = MovementLayer.All;
          layout.Owner = MovementType.None;
        }
      }
    }
  }
}