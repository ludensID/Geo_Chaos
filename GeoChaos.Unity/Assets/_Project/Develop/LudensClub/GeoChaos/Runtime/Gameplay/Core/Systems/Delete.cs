using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class Delete<TComponent, TWrapper> : IEcsRunSystem where TComponent : struct, IEcsComponent
    where TWrapper : IEcsWorldWrapper
  {
    private readonly EcsWorld _world;
    private readonly EcsEntities _deletes;

    public Delete(TWrapper wrapper)
    {
      _world = wrapper.World;

      _deletes = _world
        .Filter<TComponent>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity delete in _deletes)
        delete.Del<TComponent>();
    }
  }
}