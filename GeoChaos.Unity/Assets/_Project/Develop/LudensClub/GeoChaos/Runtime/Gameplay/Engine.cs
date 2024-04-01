using System;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Gameplay
{
  public class Engine : IInitializable, ITickable, IDisposable
  {
    private readonly IEcsSystemFactory _factory;
    private EcsWorld _world;
    private EcsSystems _systems;

    public Engine(IEcsSystemFactory factory, GameWorldWrapper gameWorldWrapper)
    {
      _factory = factory;

      _world = gameWorldWrapper.World;
      _systems = new EcsSystems(_world);

      _systems
        .Add(_factory.Create<HeroFeature>());
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