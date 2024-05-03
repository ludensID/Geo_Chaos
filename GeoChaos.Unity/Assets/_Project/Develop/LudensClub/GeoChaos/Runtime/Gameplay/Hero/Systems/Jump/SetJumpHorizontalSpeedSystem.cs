using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class SetJumpHorizontalSpeedSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly HeroConfig _config;
    private readonly EcsEntities _onGrounds;
    private readonly EcsEntities _onNotGrounds;

    public SetJumpHorizontalSpeedSystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _onGrounds = _game
        .Filter<HorizontalSpeed>()
        .Inc<OnLanded>()
        .Collect();

      _onNotGrounds = _game
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

      foreach (EcsEntity onNotGround in _onNotGrounds)
      {
        SetHorizontalSpeed(onNotGround, _config.JumpHorizontalSpeed);
      }
    }

    private void SetHorizontalSpeed(EcsEntity entity, float value)
    {
      entity.Replace((ref HorizontalSpeed speed) => speed.Value = value);
    }
  }
}