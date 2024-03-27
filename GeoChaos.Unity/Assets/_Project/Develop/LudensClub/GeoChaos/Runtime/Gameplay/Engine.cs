using System;
using Leopotam.EcsLite;
using Leopotam.EcsLite.UnityEditor;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Gameplay
{
  public class Engine : IInitializable, ITickable, IDisposable
  {
    private readonly IEcsSystemFactory _factory;
    private readonly IEcsWorldProvider _provider;
    private EcsWorld _world;
    private EcsSystems _systems;

    public Engine(IEcsSystemFactory factory, IEcsWorldProvider provider)
    {
      _factory = factory;
      _provider = provider;

      _world = new EcsWorld();
      _systems = new EcsSystems(_world);

      _provider.World = _world;

      _systems
        .Add(_factory.Create<CreatePlayerSystem>())
#if UNITY_EDITOR
        .Add(new EcsWorldDebugSystem(entityNameFormat: "D8"))
#endif
        ;
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

      if (_world != null)
      {
        _world.Destroy();
        _world = null;
      }
    }
  }
}