using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Input
{
  public class MarkExpireUpEntitiesSystem : IEcsRunSystem
  {
    private readonly EcsWorld _input;
    private readonly HeroConfig _config;
    private readonly EcsEntities _expirables;

    public MarkExpireUpEntitiesSystem(InputWorldWrapper inputWorldWrapper, IConfigProvider configProvider)
    {
      _input = inputWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _expirables = _input
        .Filter<ExpireTimer>()
        .Exc<ExpireUp>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity expirable in _expirables
        .Check<ExpireTimer>(x => x.PassedTime >= _config.MovementResponseDelay))
      {
        expirable.Add<ExpireUp>();
      }
    }
  }
}