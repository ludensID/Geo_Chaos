using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Selection;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Ring
{
  public class SelectNearestRingSystem : IEcsRunSystem
  {
    private readonly RingSelector _selector;
    private readonly EcsWorld _game;
    private readonly EcsEntities _rings;
    private readonly EcsEntities _heroes;
    private readonly EcsEntities _markedRings;

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

      _markedRings = _game
        .Filter<RingTag>()
        .Inc<Selectable>()
        .Inc<Marked>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      _selector.Select<Selected>(_heroes, _rings, _markedRings);
    }
  }
}