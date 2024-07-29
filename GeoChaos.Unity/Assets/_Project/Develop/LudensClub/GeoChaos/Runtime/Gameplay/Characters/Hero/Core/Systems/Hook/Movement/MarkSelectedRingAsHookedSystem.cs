using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Ring;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Systems.Hook
{
  public class MarkSelectedRingAsHookedSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _commands;
    private readonly EcsEntities _selectedRings;

    public MarkSelectedRingAsHookedSystem(GameWorldWrapper gameWorldWrapper)
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
      foreach (EcsEntity _ in _commands)
      foreach (EcsEntity ring in _selectedRings)
      {
        ring
          .Del<Selected>()
          .Del<Selectable>()
          .Add<Hooked>();
      }
    }
  }
}