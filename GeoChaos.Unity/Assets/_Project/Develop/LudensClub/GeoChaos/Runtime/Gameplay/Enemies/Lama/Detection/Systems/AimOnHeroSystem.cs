using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Selection;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemies.Lama.Detection
{
  public class AimOnHeroSystem : IEcsRunSystem
  {
    private readonly AimedLamaSelector _selector;
    private readonly EcsWorld _game;
    private readonly EcsEntities _heroes;
    private readonly EcsEntities _lamas;
    private readonly EcsEntities _markedLamas;

    public AimOnHeroSystem(GameWorldWrapper gameWorldWrapper, AimedLamaSelector selector)
    {
      _selector = selector;
      _game = gameWorldWrapper.World;

      _heroes = _game
        .Filter<HeroTag>()
        .Collect();
      
      _lamas = _game
        .Filter<LamaTag>()
        .Collect();

      _markedLamas = _game
        .Filter<LamaTag>()
        .Inc<Marked>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      _selector.Select<Aimed>(_heroes, _lamas, _markedLamas);
    }
  }
}