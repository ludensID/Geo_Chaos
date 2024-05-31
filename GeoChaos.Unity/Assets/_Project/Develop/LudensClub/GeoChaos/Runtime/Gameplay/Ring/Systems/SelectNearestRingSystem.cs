using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Ring
{
  public class SelectNearestRingSystem : IEcsRunSystem
  {
    private readonly RingSelector _selector;
    private readonly EcsWorld _game;
    private readonly EcsEntities _rings;
    private readonly EcsEntities _heroes;
    private readonly EcsEntities _selectedRings;

    public SelectNearestRingSystem(GameWorldWrapper gameWorldWrapper, RingSelector selector)
    {
      _selector = selector;
      _game = gameWorldWrapper.World;

      _heroes = _game
        .Filter<HeroTag>()
        .Inc<HookAvailable>()
        .Inc<ViewRef>()
        .Collect();

      _rings = _game
        .Filter<RingTag>()
        .Inc<Selectable>()
        .Collect();

      _selectedRings = _game
        .Filter<RingTag>()
        .Inc<Selected>()
        .Inc<Selectable>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      _selector.Select(_heroes, _rings, _selectedRings);
    }
  }
}