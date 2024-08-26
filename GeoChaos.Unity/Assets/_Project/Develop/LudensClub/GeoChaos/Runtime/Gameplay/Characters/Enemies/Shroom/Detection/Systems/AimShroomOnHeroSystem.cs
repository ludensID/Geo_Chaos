using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Detection;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Selection;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Shroom.Detection
{
  public class AimShroomOnHeroSystem : IEcsRunSystem
  {
    private readonly TargetInBoundsSelector _selector;
    private readonly EcsWorld _game;
    private readonly EcsEntities _heroes;
    private readonly EcsEntities _shrooms;
    private readonly EcsEntities _markedShrooms;

    public AimShroomOnHeroSystem(GameWorldWrapper gameWorldWrapper, TargetInBoundsSelector selector)
    {
      _selector = selector;
      _game = gameWorldWrapper.World;

      _heroes = _game
        .Filter<HeroTag>()
        .Collect();

      _shrooms = _game
        .Filter<ShroomTag>()
        .Collect();

      _markedShrooms = _game
        .Filter<ShroomTag>()
        .Inc<Marked>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      _selector.Select<Aimed>(_heroes, _shrooms, _markedShrooms);
    }
  }
}