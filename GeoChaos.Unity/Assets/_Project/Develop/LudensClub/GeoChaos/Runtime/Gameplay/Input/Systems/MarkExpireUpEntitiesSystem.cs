using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Input
{
  public class MarkExpireUpEntitiesSystem : IEcsRunSystem
  {
    private readonly EcsWorld _world;
    private readonly EcsFilter _expirable;
    private readonly HeroConfig _config;

    public MarkExpireUpEntitiesSystem(InputWorldWrapper inputWorldWrapper, IConfigProvider configProvider)
    {
      _world = inputWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _expirable = _world
        .Filter<ExpireTimer>()
        .Exc<ExpireUp>()
        .End();
    }

    public void Run(EcsSystems systems)
    {
      foreach (int expirable in _expirable)
      {
        ref ExpireTimer timer = ref _world.Get<ExpireTimer>(expirable);
        if (timer.PassedTime >= _config.MovementResponseDelay)
          _world.Add<ExpireUp>(expirable);
      }
    }
  }
}