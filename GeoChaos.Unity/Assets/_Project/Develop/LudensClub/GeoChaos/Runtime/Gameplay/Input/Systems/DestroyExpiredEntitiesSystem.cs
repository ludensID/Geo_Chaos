using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Input
{
  public class DestroyExpiredEntitiesSystem : IEcsRunSystem
  {
    private readonly EcsWorld _world;
    private readonly EcsFilter _expireds;

    public DestroyExpiredEntitiesSystem(InputWorldWrapper inputWorldWrapper)
    {
      _world = inputWorldWrapper.World;

      _expireds = _world
        .Filter<Expired>()
        .End();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (int expired in _expireds)
      {
        _world.DelEntity(expired);
      }
    }
  }
}