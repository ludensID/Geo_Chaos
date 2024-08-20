using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Detection.Selection;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Selection;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Detection
{
  public class AimZombieOnHeroSystem : IEcsRunSystem
  {
    private readonly TargetInBoundsSelector _selector;
    private readonly EcsWorld _game;
    private readonly EcsEntities _heroes;
    private readonly EcsEntities _zombies;
    private readonly EcsEntities _markedZombies;

    public AimZombieOnHeroSystem(GameWorldWrapper gameWorldWrapper, TargetInBoundsSelector selector)
    {
      _selector = selector;
      _game = gameWorldWrapper.World;

      _heroes = _game
        .Filter<HeroTag>()
        .Collect();

      _zombies = _game
        .Filter<ZombieTag>()
        .Collect();

      _markedZombies = _game
        .Filter<ZombieTag>()
        .Inc<Marked>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      _selector.Select<Aimed>(_heroes, _zombies, _markedZombies);
    }
  }
}