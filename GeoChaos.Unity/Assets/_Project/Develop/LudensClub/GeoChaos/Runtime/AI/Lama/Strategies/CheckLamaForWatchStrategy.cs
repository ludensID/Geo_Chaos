﻿using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Enemies.Lama.Chase;
using LudensClub.GeoChaos.Runtime.Gameplay.Enemies.Lama.Watch;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.BehaviourTrees;

namespace LudensClub.GeoChaos.Runtime.AI
{
  public class CheckLamaForWatchStrategy : IConditionStrategy
  {
    private readonly EcsWorld _game;
    public EcsPackedEntity Entity { get; set; }

    public CheckLamaForWatchStrategy(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;
    }
    
    public bool Check()
    {
      return Entity.TryUnpackEntity(_game, out EcsEntity lama)
        && (lama.Has<StopChaseCommand>() || lama.Has<WatchingTimer>());
    }
  }
}