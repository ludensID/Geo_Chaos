﻿using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Wait;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Wait
{
  public class StopLeafySpiritWaitingSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _waitingSpirits;

    public StopLeafySpiritWaitingSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _waitingSpirits = _game
        .Filter<LeafySpiritTag>()
        .Inc<StopWaitCommand>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity spirit in _waitingSpirits)
      {
        spirit
          .Del<StopWaitCommand>()
          .Del<WaitingTimer>();
      }
    }
  }
}