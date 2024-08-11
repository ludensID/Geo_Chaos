using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Damage;
using LudensClub.GeoChaos.Runtime.Gameplay.Environment.Ring;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Selection;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies
{
  public class SelectNearestDamageableEntitySystem : IEcsRunSystem
  {
    private readonly DamagableEntitySelector _selector;
    private readonly EcsWorld _game;
    private readonly EcsEntities _heroes;
    private readonly EcsEntities _damageableEntities;
    private readonly EcsEntities _markedDamageableEntities;

    public SelectNearestDamageableEntitySystem(GameWorldWrapper gameWorldWrapper, DamagableEntitySelector selector)
    {
      _selector = selector;
      _game = gameWorldWrapper.World;

      _heroes = _game
        .Filter<HeroTag>()
        .Collect();

      _damageableEntities = _game
        .Filter<EnemyTag>()
        .Inc<Damageable>()
        .Collect();

      _markedDamageableEntities = _game
        .Filter<EnemyTag>()
        .Inc<Damageable>()
        .Inc<Marked>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      _selector.Select<Selected>(_heroes, _damageableEntities, _markedDamageableEntities);
    }
  }
}