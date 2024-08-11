using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Move;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Gravity;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Jump
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
        .Filter<HeroTag>()
        .Inc<HorizontalSpeed>()
        .Inc<OnLanded>()
        .Collect();

      _onLeftGrounds = _game
        .Filter<HeroTag>()
        .Inc<HorizontalSpeed>()
        .Inc<OnLifted>()
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