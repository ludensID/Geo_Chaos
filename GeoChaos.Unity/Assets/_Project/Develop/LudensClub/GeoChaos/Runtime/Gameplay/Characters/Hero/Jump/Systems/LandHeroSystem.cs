using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Gravity;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Jump
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
        .Filter<HeroTag>()
        .Inc<Falling>()
        .Inc<MovementVector>()
        .Inc<GravityScale>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity falling in _fallings
        .Check<MovementVector>(x => x.Direction.y >= 0))
      {
        falling
          .Del<Falling>()
          .Change((ref GravityScale gravity) => gravity.Scale.Value = _config.GravityScale);
      }
    }
  }
}