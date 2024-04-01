#if UNITY_EDITOR
using System;
using Leopotam.EcsLite;
using Leopotam.EcsLite.UnityEditor;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Debugging
{
  public class EcsWorldDebugEngine : IInitializable, ITickable, IDisposable
  {
    private readonly IEcsSystemFactory _factory;
    private readonly EcsWorld _world;
    private EcsSystems _systems;

    public EcsWorldDebugEngine(GameWorldWrapper gameWorldWrapper, IEcsSystemFactory factory)
    {
      _factory = factory;
      _world = gameWorldWrapper.World;
      _systems = new EcsSystems(_world);

      _systems.Add(new EcsWorldDebugSystem(entityNameFormat: "D8"));
    }

    public void Initialize()
    {
      _systems.Init();
    }

    public void Tick()
    {
      _systems.Run();
    }

    public void Dispose()
    {
      if (_systems != null)
      {
        _systems.Destroy();
        _systems = null;
      }
    }
  }
}
#endif