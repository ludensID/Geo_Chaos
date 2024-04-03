#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.UnityEditor;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Debugging
{
  public class EcsWorldDebugEngine : IInitializable, ITickable, IDisposable
  {
    private EcsSystems _systems;

    public EcsWorldDebugEngine(GameWorldWrapper gameWorldWrapper, List<IWorldWrapper> wrappers)
    {
      wrappers.Remove(gameWorldWrapper);
      _systems = new EcsSystems(gameWorldWrapper.World);
      _systems.Add(new EcsWorldDebugSystem(entityNameFormat: "D8"));

      foreach (IWorldWrapper wrapper in wrappers)
      {
        _systems.AddWorld(wrapper.World, wrapper.Name)
          .Add(new EcsWorldDebugSystem(worldName: wrapper.Name, entityNameFormat: "D8"));
      }
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