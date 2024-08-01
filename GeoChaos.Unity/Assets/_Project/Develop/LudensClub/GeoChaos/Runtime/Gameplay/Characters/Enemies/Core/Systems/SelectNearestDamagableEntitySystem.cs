using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Characteristics.Health;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Ring;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Selection;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies
{
  public class SelectNearestDamagableEntitySystem : IEcsRunSystem
  {
    private readonly DamagableEntitySelector _selector;
    private readonly EcsWorld _game;
    private readonly EcsEntities _heroes;
    private readonly EcsEntities _damagables;
    private readonly EcsEntities _markedDamagables;

    public SelectNearestDamagableEntitySystem(GameWorldWrapper gameWorldWrapper, DamagableEntitySelector selector)
    {
      _selector = selector;
      _game = gameWorldWrapper.World;

      _heroes = _game
        .Filter<HeroTag>()
        .Collect();

      _damagables = _game
        .Filter<CurrentHealth>()
        .Exc<HeroTag>()
        .Collect();

      _markedDamagables = _game
        .Filter<CurrentHealth>()
        .Inc<Marked>()
        .Exc<HeroTag>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      _selector.Select<Selected>(_heroes, _damagables, _markedDamagables);
    }
  }
}