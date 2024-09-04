using System.Collections.Generic;
using System.Linq;
using Leopotam.EcsLite;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public class EcsFeature : IEcsPreInitSystem, IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem, IEcsPostDestroySystem
  {
#if UNITY_EDITOR && !DISABLE_PROFILING
    private static readonly System.Type _featureType = typeof(EcsFeature);
    private static readonly System.Type _systemType = typeof(IEcsSystem);
#endif

    private readonly List<IEcsSystem> _systems = new List<IEcsSystem>();
    private readonly List<IEcsRunSystem> _runSystems = new List<IEcsRunSystem>();

    public void Add(IEcsSystem system)
    {
      _systems.Add(system);
    }

    public void PreInit(EcsSystems systems)
    {
      foreach (IEcsPreInitSystem system in _systems.OfType<IEcsPreInitSystem>())
        system.PreInit(systems);

      foreach (IEcsRunSystem system in _systems.OfType<IEcsRunSystem>())
        _runSystems.Add(system);
    }

    public void Init(EcsSystems systems)
    {
      foreach (IEcsInitSystem system in _systems.OfType<IEcsInitSystem>())
        system.Init(systems);
    }

    public void Run(EcsSystems systems)
    {
#if UNITY_EDITOR && !DISABLE_PROFILING
      using (new Unity.Profiling.ProfilerMarker(EditorContext.GetPrettyName(this, nameof(Run), _featureType)).Auto())
#endif
      {
        foreach (IEcsRunSystem system in _runSystems)
        {
#if UNITY_EDITOR && !DISABLE_PROFILING
          if (system is not EcsFeature)
            using (new Unity.Profiling.ProfilerMarker(EditorContext.GetPrettyName(system, "Run", _systemType)).Auto())
              RunSystem();
          else
            RunSystem();

          void RunSystem()
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