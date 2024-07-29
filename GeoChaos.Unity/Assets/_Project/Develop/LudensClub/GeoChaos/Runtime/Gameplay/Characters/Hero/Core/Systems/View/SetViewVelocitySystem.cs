using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.View;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Systems.View
{
  public class SetViewVelocitySystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _heroes;

    public SetViewVelocitySystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;
      _heroes = _game
        .Filter<RigidbodyRef>()
        .Inc<MovementVector>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity hero in _heroes)
      {
        ref MovementVector vector = ref hero.Get<MovementVector>();
        Vector2 velocity = vector.Direction * vector.Speed;
        hero.Change((ref RigidbodyRef rigidbodyRef) => rigidbodyRef.Rigidbody.velocity = velocity);
      }
    }
  }
}