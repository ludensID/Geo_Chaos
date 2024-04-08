using System.Collections.Generic;
using System.Linq;
using Leopotam.EcsLite;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public class EcsFeature : IEcsPreInitSystem, IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem, IEcsPostDestroySystem
  {
    private readonly List<IEcsSystem> _systems = new();

    public void Add(IEcsSystem system)
    {
      _systems.Add(system);
    }

    public void PreInit(EcsSystems systems)
    {
      foreach (var system in _systems.OfType<IEcsPreInitSystem>()) system.PreInit(systems);
    }

    public void Init(EcsSystems systems)
    {
      foreach (var system in _systems.OfType<IEcsInitSystem>()) system.Init(systems);
    }

    public void Run(EcsSystems systems)
    {
      foreach (var system in _systems.OfType<IEcsRunSystem>()) system.Run(systems);
    }

    public void Destroy(EcsSystems systems)
    {
      foreach (var system in _systems.OfType<IEcsDestroySystem>()) system.Destroy(systems);
    }

    public void PostDestroy(EcsSystems systems)
    {
      foreach (var system in _systems.OfType<IEcsPostDestroySystem>()) system.PostDestroy(systems);
    }
  }
}