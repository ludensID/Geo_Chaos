using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Input
{
  public class MarkExpiredEntitySystem : IEcsRunSystem
  {
    private readonly EcsWorld _input;
    private readonly EcsEntities _expireUps;
    private readonly EcsEntity _expiredInput;

    public MarkExpiredEntitySystem(InputWorldWrapper inputWorldWrapper)
    {
      _input = inputWorldWrapper.World;

      _expireUps = _input
        .Filter<ExpireUp>()
        .Collect();

      _expiredInput = new EcsEntity(_input, -1);
    }

    public void Run(EcsSystems systems)
    {
      float maxTime = 0;
      _expiredInput.Entity = -1;

      foreach (EcsEntity expireUp in _expireUps)
      {
        float time = expireUp.Get<ExpireTimer>().PassedTime;
        if (time > maxTime)
        {
          maxTime = time;
          _expiredInput.Entity = expireUp.Entity;
        }
      }

      if (_expiredInput.IsAlive())
        _expiredInput.Add<Expired>();
    }
  }
}