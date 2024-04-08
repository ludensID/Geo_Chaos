using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class FallHeroSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsFilter _onGrounds;
    private readonly HeroConfig _config;

    public FallHeroSystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _onGrounds = _game
        .Filter<HeroMovementVector>()
        .Inc<GravityScale>()
        .Exc<IsFalling>()
        .End();
    }

    public void Run(EcsSystems systems)
    {
      foreach (int onGround in _onGrounds)
      {
        ref HeroMovementVector vector = ref _game.Get<HeroMovementVector>(onGround);
        if (vector.Direction.y <= 0)
        {
          if(_game.Has<IsJumping>(onGround))
            _game.Del<IsJumping>(onGround);
          AddFallingComponents(onGround);
        }
      }
    }

    private void AddFallingComponents(int entity)
    {
      _game.Add<IsFalling>(entity);
      ref GravityScale gravityScale = ref _game.Get<GravityScale>(entity);
      gravityScale.Value = _config.FallGravityScale;
      gravityScale.Override = true;
    }
  }
}