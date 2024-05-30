using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Input
{
  public class DestroyExpiredEntitiesSystem : IEcsRunSystem
  {
    private readonly EcsWorld _world;
    private readonly EcsEntities _expireds;

    public DestroyExpiredEntitiesSystem(InputWorldWrapper inputWorldWrapper)
    {
      _world = inputWorldWrapper.World;

      _expireds = _world
        .Filter<Expired>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity expired in _expireds) 
        expired.Dispose();
    }
  }
}