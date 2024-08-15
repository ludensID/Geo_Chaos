using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Bump;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Bump
{
  public class StopFreezeBodySystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsWorld _physics;
    private readonly EcsEntities _frozenHeroes;
    private readonly EcsEntities _forces;

    public StopFreezeBodySystem(GameWorldWrapper gameWorldWrapper, PhysicsWorldWrapper physicsWorldWrapper)
    {
      _game = gameWorldWrapper.World;
      _physics = physicsWorldWrapper.World;

      _frozenHeroes = _game
        .Filter<HeroTag>()
        .Inc<BodyFreezing>()
        .Exc<Bumping>()
        .Collect();

      _forces = _physics
        .Filter<SpeedForce>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity hero in _frozenHeroes)
      {
        _forces.Check<Owner>(x => x.Entity.EqualsTo(hero.PackedEntity));
        if (_forces.Any() || hero.Get<MovementVector>().Speed == Vector2.zero)
          hero.Del<BodyFreezing>();
      }
    }
  }
}