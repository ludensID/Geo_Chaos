using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class LandHeroSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly HeroConfig _config;
    private readonly EcsEntities _fallings;

    public LandHeroSystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _fallings = _game
        .Filter<Falling>()
        .Inc<MovementVector>()
        .Inc<GravityScale>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity falling in _fallings
        .Where<MovementVector>(x => x.Direction.y >= 0))
      {
        falling
          .Del<Falling>()
          .Replace((ref GravityScale gravity) => gravity.Value = _config.GravityScale);
      }
    }
  }
}