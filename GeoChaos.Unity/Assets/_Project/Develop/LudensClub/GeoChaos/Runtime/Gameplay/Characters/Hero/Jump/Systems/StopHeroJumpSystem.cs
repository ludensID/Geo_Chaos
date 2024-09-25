using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Jump;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Jump
{
  public class StopHeroJumpSystem : IEcsRunSystem
  {
    private readonly ISpeedForceFactory _forceFactory;
    private readonly EcsWorld _world;
    private readonly EcsEntities _heroes;

    public StopHeroJumpSystem(GameWorldWrapper gameWorldWrapper, ISpeedForceFactory forceFactory)
    {
      _forceFactory = forceFactory;
      _world = gameWorldWrapper.World;

      _heroes = _world
        .Filter<HeroTag>()
        .Inc<StopJumpCommand>()
        .Inc<Jumping>()
        .Inc<MovementVector>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity hero in _heroes)
      {
        _forceFactory.Create(new SpeedForceData(SpeedForceType.Jump, hero.PackedEntity, Vector2.up)
        {
          Speed = new Vector2(0, 0),
          Instant = true
        });
        
        hero
          .Del<Jumping>()
          .Change((ref MovementLayout layout) => layout.Movement = MovementType.None)
          .Del<StopJumpCommand>();
      }
    }
  }
}