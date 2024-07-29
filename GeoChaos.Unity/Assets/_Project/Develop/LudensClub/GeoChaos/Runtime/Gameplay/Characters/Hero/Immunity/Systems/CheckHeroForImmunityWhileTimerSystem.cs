using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Immunity
{
  public class CheckHeroForImmunityWhileTimerSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _immuneHeroes;

    public CheckHeroForImmunityWhileTimerSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _immuneHeroes = _game
        .Filter<HeroTag>()
        .Inc<ImmunityTimer>()
        .Exc<Immune>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity hero in _immuneHeroes)
      {
        hero.Add((ref Immune immune) => immune.Owner = MovementType.Bump);
      }
    }
  }
}