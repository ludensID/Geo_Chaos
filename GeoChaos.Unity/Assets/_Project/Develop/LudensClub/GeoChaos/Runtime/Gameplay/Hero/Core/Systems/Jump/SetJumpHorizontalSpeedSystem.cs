using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class SetJumpHorizontalSpeedSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly HeroConfig _config;
    private readonly EcsEntities _onGrounds;
    private readonly EcsEntities _onLeftGrounds;

    public SetJumpHorizontalSpeedSystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _onGrounds = _game
        .Filter<HorizontalSpeed>()
        .Inc<OnLanded>()
        .Collect();

      _onLeftGrounds = _game
        .Filter<HorizontalSpeed>()
        .Inc<OnLeftGround>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity onGround in _onGrounds)
      {
        SetHorizontalSpeed(onGround, _config.MovementSpeed);
      }

      foreach (EcsEntity onNotGround in _onLeftGrounds)
      {
        SetHorizontalSpeed(onNotGround, _config.JumpHorizontalSpeed);
      }
    }

    private void SetHorizontalSpeed(EcsEntity entity, float value)
    {
      entity.Change((ref HorizontalSpeed speed) => speed.Speed = value);
    }
  }
}