using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

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
        .Exc<IsFalling>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity onGround in _onGrounds
        .Where<MovementVector>(x => x.Direction.y <= 0))
      {
        if (onGround.Is<IsJumping>())
          onGround.Del<IsJumping>();
        
        onGround.Add<IsFalling>()
          .Replace((ref GravityScale gravity) =>
          {
            gravity.Value = _config.FallGravityScale;
            gravity.Override = true;
          });
      }
    }
  }
}