using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Jump
{
  public class ClampFallHeroVelocitySystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _fallingHeroes;
    private readonly HeroConfig _config;

    public ClampFallHeroVelocitySystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _fallingHeroes = _game
        .Filter<HeroTag>()
        .Inc<Falling>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity hero in _fallingHeroes)
      {
        ref MovementVector vector = ref hero.Get<MovementVector>();
        Vector2 velocity = vector.Speed * vector.Direction;
        velocity.y = MathUtils.Clamp(velocity.y, -_config.MaxFallVelocity);
        vector.AssignVector(velocity);
      }
    }
  }
}