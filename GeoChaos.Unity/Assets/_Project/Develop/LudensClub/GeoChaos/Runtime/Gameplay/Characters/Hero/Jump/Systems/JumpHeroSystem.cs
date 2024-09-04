using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Jump;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Jump
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
        .Filter<HeroTag>()
        .Inc<JumpAvailable>()
        .Inc<JumpCommand>()
        .Inc<MovementVector>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity hero in _heroes)
      {
        _forceFactory.Create(new SpeedForceData(SpeedForceType.Jump, hero.PackedEntity, Vector2.up)
        {
          Speed = Vector2.up * _config.JumpForce,
          Direction = Vector2.up,
          Instant = true
        });
        
        hero
          .Add<Jumping>()
          .Del<JumpCommand>()
          .Replace((ref ActionState actionState) => actionState.StartNew())
          .Replace((ref ActionContext ctx) => ctx.IsEmpty = true);
      }
    }
  }
}