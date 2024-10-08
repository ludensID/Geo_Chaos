﻿using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Detection;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Watch;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Detection;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Stun;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Watch
{
  public class FrogWatchWhenWasAimedSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _frogs;

    public FrogWatchWhenWasAimedSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _frogs = _game
        .Filter<FrogTag>()
        .Exc<WatchCommand>()
        .Exc<Stunned>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity frog in _frogs)
      {
        if (frog.Has<WasTargetInView>() && !frog.Has<TargetInView>()
          || frog.Has<WasTargetInFront>() && !frog.Has<TargetInFront>())
        {
          frog.Add<WatchCommand>();
        }
      }
    }
  }
}