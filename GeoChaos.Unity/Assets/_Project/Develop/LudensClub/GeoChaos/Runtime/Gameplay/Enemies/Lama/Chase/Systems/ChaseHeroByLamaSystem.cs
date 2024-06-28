using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemies.Lama.Chase
{
  public class ChaseHeroByLamaSystem : IEcsRunSystem
  {
    private readonly ISpeedForceFactory _forceFactory;
    private readonly EcsWorld _game;
    private readonly EcsEntities _lamas;
    private readonly LamaConfig _config;

    public ChaseHeroByLamaSystem(GameWorldWrapper gameWorldWrapper,
      ISpeedForceFactory forceFactory,
      IConfigProvider configProvider)
    {
      _forceFactory = forceFactory;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<LamaConfig>();

      _lamas = _game
        .Filter<LamaTag>()
        .Inc<ChaseCommand>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity lama in _lamas)
      {
        if (!lama.Has<Chasing>())
        {
          _forceFactory.Create(new SpeedForceData(SpeedForceType.Chase, lama.Pack(), Vector2.right)
          {
            Speed = new Vector2(_config.MovementSpeed, 0),
            Direction = Vector2.one,
            Unique = true
          });

          lama.Add<Chasing>();
        }

        lama.Del<ChaseCommand>();
      }
    }
  }
}