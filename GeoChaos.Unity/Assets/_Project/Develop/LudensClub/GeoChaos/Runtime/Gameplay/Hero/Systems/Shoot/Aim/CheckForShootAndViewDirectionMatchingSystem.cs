using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Shoot;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Shoot.Aim;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Shoot.Aim
{
  public class CheckForShootAndViewDirectionMatchingSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _aimings;

    public CheckForShootAndViewDirectionMatchingSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _aimings = _game
        .Filter<Aiming>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity aiming in _aimings)
      {
        ref ViewDirection viewDir = ref aiming.Get<ViewDirection>();
        float shootDirectionX = aiming.Get<ShootDirection>().Direction.x;
        if (viewDir.Direction.x * shootDirectionX < 0)
          viewDir.Direction.x = shootDirectionX;
      }
    }
  }
}