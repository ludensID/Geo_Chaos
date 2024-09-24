using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public class HeroHolder : IHeroHolder, ITickable
  {
    private readonly EcsEntities _heroes;
    private EcsWorld _game;
    
    public EcsEntity Hero { get; }

    public HeroHolder(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;
      Hero = new EcsEntity(_game);

      _heroes = _game
        .Filter<HeroTag>()
        .Collect();
    }

    public void Tick()
    {
      foreach (EcsEntity hero in _heroes)
      {
        Hero.Entity = hero.Entity;
      }
    }
  }
}