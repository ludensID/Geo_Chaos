﻿using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Jump;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Jump
{
  public class FilterJumpStopCommandSystem : IEcsRunSystem
  {
    private readonly EcsWorld _world;
    private readonly EcsEntities _heroes;

    public FilterJumpStopCommandSystem(GameWorldWrapper gameWorldWrapper)
    {
      _world = gameWorldWrapper.World;

      _heroes = _world
        .Filter<HeroTag>()
        .Inc<JumpAvailable>()
        .Inc<StopJumpCommand>()
        .Inc<MovementVector>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity hero in _heroes
        .Check<MovementVector>(x => x.Direction.y <= 0))
      {
        hero.Del<StopJumpCommand>();
      }
    }
  }
}