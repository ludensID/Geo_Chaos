using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Bump;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Gravity;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Bump
{
  public class ResetToZeroVelocityWhenHeroBumpingSystem : IEcsRunSystem
  {
    private readonly ISpeedForceFactory _forceFactory;
    private readonly EcsWorld _game;
    private readonly EcsEntities _bumpingHeroes;

    public ResetToZeroVelocityWhenHeroBumpingSystem(GameWorldWrapper gameWorldWrapper, ISpeedForceFactory forceFactory)
    {
      _forceFactory = forceFactory;
      _game = gameWorldWrapper.World;

      _bumpingHeroes = _game
        .Filter<HeroTag>()
        .Inc<Bumping>()
        .Inc<OnLanded>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity hero in _bumpingHeroes)
      {
        _forceFactory.Create(new SpeedForceData(SpeedForceType.Bump, hero.PackedEntity, Vector2.one)
        {
          Instant = true,
          Spare = true
        });
      }
    }
  }
}