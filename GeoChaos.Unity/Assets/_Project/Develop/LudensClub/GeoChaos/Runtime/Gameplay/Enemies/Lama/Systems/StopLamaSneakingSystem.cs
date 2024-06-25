using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemies.Lama
{
  public class StopLamaSneakingSystem : IEcsRunSystem
  {
    private readonly ISpeedForceFactory _forceFactory;
    private readonly EcsWorld _game;
    private readonly EcsEntities _lamas;

    public StopLamaSneakingSystem(GameWorldWrapper gameWorldWrapper, ISpeedForceFactory forceFactory)
    {
      _forceFactory = forceFactory;
      _game = gameWorldWrapper.World;

      _lamas = _game
        .Filter<LamaTag>()
        .Inc<StopSneakCommand>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity lama in _lamas)
      {
        if (lama.Has<Sneaking>())
        {
          lama
            .Del<Sneaking>()
            .Has<SneakingTimer>(false);

          _forceFactory.Create(new SpeedForceData(SpeedForceType.Sneak, lama.Pack(), Vector2.right)
          {
            Instant = true
          });
        }

        lama.Del<StopSneakCommand>();
      }
    }
  }
}