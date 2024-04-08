using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Input
{
  public class UpdateExpireTimeSystem : IEcsRunSystem
  {
    private readonly EcsWorld _world;
    private readonly EcsFilter _timers;

    public UpdateExpireTimeSystem(InputWorldWrapper inputWorldWrapper)
    {
      _world = inputWorldWrapper.World;

      _timers = _world
        .Filter<ExpireTimer>()
        .End();
    }

    public void Run(EcsSystems systems)
    {
      foreach (var timer in _timers)
      {
        ref var expireTimer = ref _world.Get<ExpireTimer>(timer);
        expireTimer.PassedTime += Time.deltaTime;
      }
    }
  }
}