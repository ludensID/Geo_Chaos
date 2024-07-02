using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Immunity
{
  public class DepriveHeroOfImmunitySystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _immuneHeroes;

    public DepriveHeroOfImmunitySystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _immuneHeroes = _game
        .Filter<HeroTag>()
        .Inc<ImmunityTimer>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity hero in _immuneHeroes
        .Where<ImmunityTimer>(x => x.TimeLeft <= 0))
      {
        hero
          .Del<ImmunityTimer>()
          .Del<Immune>()
          .Add<OnImmunityFinished>();
      }
    }
  }
}