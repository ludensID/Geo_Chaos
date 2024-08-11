using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Shoot;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Aim
{
  public class CheckForHeroShootAndViewDirectionMatchingSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _aimingHeroes;

    public CheckForHeroShootAndViewDirectionMatchingSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _aimingHeroes = _game
        .Filter<HeroTag>()
        .Inc<Aiming>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity aiming in _aimingHeroes)
      {
        ref ViewDirection viewDir = ref aiming.Get<ViewDirection>();
        float shootDirectionX = aiming.Get<ShootDirection>().Direction.x;
        if (viewDir.Direction.x * shootDirectionX < 0)
          viewDir.Direction.x = shootDirectionX;
      }
    }
  }
}