using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Gravity;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Aim
{
  public class CheckForHeroOnGroundToAimSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _liftedHeroes;

    public CheckForHeroOnGroundToAimSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _liftedHeroes = _game
        .Filter<HeroTag>()
        .Inc<OnLifted>()
        .Inc<Aiming>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity left in _liftedHeroes)
      {
        left.Add<FinishAimCommand>();
      }
    }
  }
}