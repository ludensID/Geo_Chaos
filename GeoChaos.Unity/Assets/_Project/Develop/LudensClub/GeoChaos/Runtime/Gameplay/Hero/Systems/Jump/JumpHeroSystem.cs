using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class JumpHeroSystem : IEcsRunSystem
  {
    private readonly ISpeedForceFactory _forceFactory;
    private readonly EcsWorld _game;
    private readonly HeroConfig _config;
    private readonly EcsEntities _heroes;

    public JumpHeroSystem(GameWorldWrapper gameWorldWrapper,
      ISpeedForceFactory forceFactory,
      IConfigProvider configProvider)
    {
      _forceFactory = forceFactory;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _heroes = _game
        .Filter<JumpAvailable>()
        .Inc<JumpCommand>()
        .Inc<MovementVector>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity hero in _heroes)
      {
        _forceFactory.Create(new SpeedForceData(SpeedForceType.Jump, hero.Pack(), Vector2.up)
        {
          Speed = new Vector2(0, _config.JumpForce),
          Direction = new Vector2(0, 1),
          Instant = true
        });

        hero
          .Add<Jumping>()
          .Del<JumpCommand>();
      }
    }
  }
}