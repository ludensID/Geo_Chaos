﻿using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Move;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Move
{
  public class IgnoreMovementSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _movings;
    private readonly SpeedForceLoop _forceLoop;

    public IgnoreMovementSystem(GameWorldWrapper gameWorldWrapper, ISpeedForceLoopService forceLoopSvc)
    {
      _game = gameWorldWrapper.World;
      _forceLoop = forceLoopSvc.CreateLoop();

      _movings = _game
        .Filter<HeroTag>()
        .Inc<Moving>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity moving in _movings)
      {
        bool shouldIgnore = moving.Get<MovementLayout>().Layer != MovementLayer.All || moving.Has<FreeFalling>();
        foreach (EcsEntity force in _forceLoop
          .GetLoop(SpeedForceType.Move, moving.PackedEntity))
        {
          force.Has<Ignored>(shouldIgnore);
        }
      }
    }
  }
}