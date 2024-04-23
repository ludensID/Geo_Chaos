using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public class Delete<TComponent, TWrapper> : IEcsRunSystem where TComponent : struct, IEcsComponent
    where TWrapper : IEcsWorldWrapper
  {
    private readonly EcsWorld _world;
    private readonly EcsFilter _deletes;

    public Delete(TWrapper wrapper)
    {
      _world = wrapper.World;

      _deletes = _world
        .Filter<TComponent>()
        .End();
    }

    public void Run(EcsSystems systems)
    {
      foreach (int delete in _deletes)
        _world.Del<TComponent>(delete);
    }
  }
}