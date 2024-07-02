using System.Collections.Generic;
using System.Linq;
using Leopotam.EcsLite;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public class EcsFeature : IEcsPreInitSystem, IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem, IEcsPostDestroySystem
  {
    private readonly List<IEcsSystem> _systems = new List<IEcsSystem>();

    public void Add(IEcsSystem system)
    {
      _systems.Add(system);
    }

    public void PreInit(EcsSystems systems)
    {
      foreach (IEcsPreInitSystem system in _systems.OfType<IEcsPreInitSystem>())
        system.PreInit(systems);
    }

    public void Init(EcsSystems systems)
    {
      foreach (IEcsInitSystem system in _systems.OfType<IEcsInitSystem>())
        system.Init(systems);
    }

    public void Run(EcsSystems systems)
    {
#if UNITY_EDITOR && !DISABLE_PROFILING
      using (new Unity.Profiling.ProfilerMarker($"{GetType().Name}.Run()").Auto())
#endif
      {
        foreach (IEcsRunSystem system in _systems.OfType<IEcsRunSystem>())
        {
#if UNITY_EDITOR && !DISABLE_PROFILING
          using (new Unity.Profiling.ProfilerMarker($"{system.GetType().Name}.Run()").Auto())
#endif
          {
            system.Run(systems);
          }
        }
      }
    }

    public void Destroy(EcsSystems systems)
    {
      foreach (IEcsDestroySystem system in _systems.OfType<IEcsDestroySystem>())
        system.Destroy(systems);
    }

    public void PostDestroy(EcsSystems systems)
    {
      foreach (IEcsPostDestroySystem system in _systems.OfType<IEcsPostDestroySystem>())
        system.PostDestroy(systems);
    }
  }
}