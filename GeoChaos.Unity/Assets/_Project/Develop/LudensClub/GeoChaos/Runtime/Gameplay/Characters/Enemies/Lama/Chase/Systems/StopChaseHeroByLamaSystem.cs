using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Lama.Chase
{
  public class StopChaseHeroByLamaSystem : IEcsRunSystem
  {
    private readonly ISpeedForceFactory _forceFactory;
    private readonly EcsWorld _game;
    private readonly EcsEntities _lamas;

    public StopChaseHeroByLamaSystem(GameWorldWrapper gameWorldWrapper, ISpeedForceFactory forceFactory)
    {
      _forceFactory = forceFactory;
      _game = gameWorldWrapper.World;

      _lamas = _game
        .Filter<LamaTag>()
        .Inc<StopChaseCommand>()
        .Inc<Chasing>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity lama in _lamas)
      {
        lama.Del<Chasing>();
        _forceFactory.Create(new SpeedForceData(SpeedForceType.Chase, lama.Pack(), Vector2.right)
        {
          Instant = true
        });
      }
    }
  }
}