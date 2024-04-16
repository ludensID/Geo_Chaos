using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Ring;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Hook
{
  public class CheckForSelectedRingSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _commands;
    private readonly EcsEntities _selectedRings;

    public CheckForSelectedRingSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _commands = _game
        .Filter<HookCommand>()
        .Collect();

      _selectedRings = _game
        .Filter<RingTag>()
        .Inc<Selected>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      bool isSelected = false;

      foreach (EcsEntity _ in _selectedRings)
      {
        isSelected = true;
      }
      
      foreach (EcsEntity command in _commands)
      {
        if (!isSelected)
          command.Del<HookCommand>();
      }
    }
  }
}