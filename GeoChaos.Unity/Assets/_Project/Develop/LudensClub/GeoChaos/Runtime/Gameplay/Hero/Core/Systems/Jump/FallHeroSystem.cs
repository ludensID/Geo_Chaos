using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class FallHeroSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly HeroConfig _config;
    private readonly EcsEntities _onGrounds;

    public FallHeroSystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _onGrounds = _game
        .Filter<MovementVector>()
        .Inc<GravityScale>()
        .Exc<Falling>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity onGround in _onGrounds
        .Where<MovementVector>(x => x.Direction.y <= 0))
      {
        if (onGround.Has<Jumping>())
          onGround.Del<Jumping>();
        
        onGround
          .Add<Falling>()
          .Change((ref GravityScale gravity) => gravity.Value = _config.FallGravityScale);
      }
    }
  }
}