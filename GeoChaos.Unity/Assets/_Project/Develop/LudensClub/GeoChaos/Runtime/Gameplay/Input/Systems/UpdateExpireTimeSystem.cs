using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Input
{
  public class UpdateExpireTimeSystem : IEcsRunSystem
  {
    private readonly EcsWorld _world;
    private readonly EcsEntities _timers;

    public UpdateExpireTimeSystem(InputWorldWrapper inputWorldWrapper)
    {
      _world = inputWorldWrapper.World;

      _timers = _world
        .Filter<ExpireTimer>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity timer in _timers)
      {
        timer.Change((ref ExpireTimer expire) => expire.PassedTime += Time.unscaledDeltaTime);
      }
    }
  }
}