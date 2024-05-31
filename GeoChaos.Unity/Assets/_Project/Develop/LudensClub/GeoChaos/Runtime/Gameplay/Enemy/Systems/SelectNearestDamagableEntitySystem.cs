using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Characteristics.Components;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Ring;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemy
{
  public class SelectNearestDamagableEntitySystem : IEcsRunSystem
  {
    private readonly DamagableEntitySelector _selector;
    private readonly EcsWorld _game;
    private readonly EcsEntities _heroes;
    private readonly EcsEntities _damagables;
    private readonly EcsEntities _selectedDamagables;

    public SelectNearestDamagableEntitySystem(GameWorldWrapper gameWorldWrapper, DamagableEntitySelector selector)
    {
      _selector = selector;
      _game = gameWorldWrapper.World;

      _heroes = _game
        .Filter<HeroTag>()
        .Collect();

      _damagables = _game
        .Filter<Health>()
        .Exc<HeroTag>()
        .Collect();

      _selectedDamagables = _game
        .Filter<Health>()
        .Inc<Selected>()
        .Exc<HeroTag>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      _selector.Select(_heroes, _damagables, _selectedDamagables);
    }
  }
}