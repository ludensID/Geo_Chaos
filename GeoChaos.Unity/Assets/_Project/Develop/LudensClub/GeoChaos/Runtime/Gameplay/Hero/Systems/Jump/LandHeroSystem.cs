using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class LandHeroSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsFilter _fallings;
    private readonly HeroConfig _config;

    public LandHeroSystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _fallings = _game
        .Filter<IsFalling>()
        .Inc<HeroMovementVector>()
        .Inc<GravityScale>()
        .End();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (int falling in _fallings)
      {
        ref HeroMovementVector vector = ref _game.Get<HeroMovementVector>(falling);
        if (vector.Direction.y >= 0)
        {
          _game.Del<IsFalling>(falling);
          ref GravityScale gravityScale = ref _game.Get<GravityScale>(falling);
          gravityScale.Value = _config.GravityScale;
          gravityScale.Override = true;
        }
      }
    }
  }
}