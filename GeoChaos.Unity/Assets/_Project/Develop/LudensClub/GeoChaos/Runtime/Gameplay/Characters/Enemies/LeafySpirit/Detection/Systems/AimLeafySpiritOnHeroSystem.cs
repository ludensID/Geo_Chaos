using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Selection;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Detection
{
  public class AimLeafySpiritOnHeroSystem : IEcsRunSystem
  {
    private readonly AimedLeafySpiritSelector _selector;
    private readonly EcsWorld _game;
    private readonly EcsEntities _heroes;
    private readonly EcsEntities _spirits;
    private readonly EcsEntities _markedSpirits;

    public AimLeafySpiritOnHeroSystem(GameWorldWrapper gameWorldWrapper, AimedLeafySpiritSelector selector)
    {
      _selector = selector;
      _game = gameWorldWrapper.World;

      _heroes = _game
        .Filter<HeroTag>()
        .Collect();
        
      _spirits = _game
        .Filter<LeafySpiritTag>()
        .Collect();

      _markedSpirits = _game
        .Filter<LeafySpiritTag>()
        .Inc<Marked>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      _selector.Select<Aimed>(_heroes, _spirits, _markedSpirits);
    }
  }
}