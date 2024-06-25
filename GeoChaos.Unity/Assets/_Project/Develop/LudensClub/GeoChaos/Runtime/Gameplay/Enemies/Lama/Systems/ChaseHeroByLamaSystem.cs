using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.AI;
using LudensClub.GeoChaos.Runtime.Gameplay.AI;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemies.Lama
{
  public class ChaseHeroByLamaSystem : IEcsRunSystem
  {
    private readonly ISpeedForceFactory _forceFactory;
    private readonly EcsWorld _game;
    private readonly EcsEntities _lamas;

    public ChaseHeroByLamaSystem(GameWorldWrapper gameWorldWrapper, ISpeedForceFactory forceFactory)
    {
      _forceFactory = forceFactory;
      _game = gameWorldWrapper.World;

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
          var ctx = lama.Get<BrainContext>().Cast<LamaContext>();

          _forceFactory.Create(new SpeedForceData(SpeedForceType.Chase, lama.Pack(), Vector2.right)
          {
            Speed = new Vector2(ctx.MovementSpeed, 0),
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