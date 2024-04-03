using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core.Dash;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class InitializeHeroMovementSystem : IEcsInitSystem
  {
    private readonly EcsWorld _world;
    private readonly EcsFilter _heroes;

    public InitializeHeroMovementSystem(GameWorldWrapper gameWorldWrapper)
    {
      _world = gameWorldWrapper.World;
      _heroes = _world.Filter<Hero>().End();
    }

    public void Init(EcsSystems systems)
    {
      foreach (int hero in _heroes)
      {
        ref Movable movable = ref _world.Add<Movable>(hero);
        movable.CanMove = true;

        ref HeroMovementVector vector = ref _world.Add<HeroMovementVector>(hero);
        vector.Direction.x = 1;
        
        _world.Add<HeroVelocity>(hero);
        _world.Add<Ground>(hero);
        _world.Add<JumpAvailable>(hero);

        ref DashAvailable dash = ref _world.Add<DashAvailable>(hero);
        dash.CanDash = true;
      }
    }
  }
}