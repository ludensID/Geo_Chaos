using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Input
{
  public class MarkExpiredEntitySystem : IEcsRunSystem
  {
    private readonly EcsWorld _world;
    private readonly EcsFilter _expireUps;

    public MarkExpiredEntitySystem(InputWorldWrapper inputWorldWrapper)
    {
      _world = inputWorldWrapper.World;

      _expireUps = _world
        .Filter<ExpireUp>()
        .End();
    }

    public void Run(EcsSystems systems)
    {
      float maxTime = 0;
      var expiredEntity = -1;

      foreach (var expireUp in _expireUps)
      {
        ref var timer = ref _world.Get<ExpireTimer>(expireUp);
        if (timer.PassedTime > maxTime)
        {
          maxTime = timer.PassedTime;
          expiredEntity = expireUp;
        }
      }

      if (expiredEntity != -1)
        _world.Add<Expired>(expiredEntity);
    }
  }
}