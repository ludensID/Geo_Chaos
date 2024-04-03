using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class JumpHeroSystem : IEcsRunSystem
  {
    private readonly EcsWorld _world;
    private readonly EcsFilter _heroes;
    private readonly HeroConfig _config;

    public JumpHeroSystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _world = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _heroes = _world.Filter<Hero>()
        .Inc<JumpAvailable>()
        .Inc<JumpCommand>()
        .Inc<HeroMovementVector>()
        .End();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (int hero in _heroes)
      {
        ref HeroMovementVector vector = ref _world.Get<HeroMovementVector>(hero);
        vector.Speed.y = _config.JumpForce;
        vector.Direction.y = 1;
        
        _world.Add<IsJumping>(hero);
        
        _world.Del<JumpCommand>(hero);
      }
    }
  }
}