using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Input
{
  public class MarkExpiredEntitySystem : IEcsRunSystem
  {
    private readonly EcsWorld _world;
    private readonly EcsEntities _expireUps;

    public MarkExpiredEntitySystem(InputWorldWrapper inputWorldWrapper)
    {
      _world = inputWorldWrapper.World;

      _expireUps = _world
        .Filter<ExpireUp>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      float maxTime = 0;
      EcsEntity expiredEntity = null;

      foreach (EcsEntity expireUp in _expireUps)
      {
        float time = expireUp.Get<ExpireTimer>().PassedTime;
        if (time > maxTime)
        {
          maxTime = time;
          expiredEntity = expireUp.Clone();
        }
      }

      expiredEntity?.Add<Expired>();
    }
  }
}