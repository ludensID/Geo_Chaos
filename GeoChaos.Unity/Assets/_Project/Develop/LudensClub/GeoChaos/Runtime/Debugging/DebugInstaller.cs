#if UNITY_EDITOR
using LudensClub.GeoChaos.Runtime.Debugging.Watchers;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Debugging
{
  public static class DebugInstaller
  {
    public static void BindInputDebug(DiContainer container)
    {
      container
        .Bind<InputDebug>()
        .FromNewComponentOnNewGameObject()
        .WithGameObjectName($"[{nameof(InputDebug)}]")
        .AsSingle()
        .NonLazy();
    }
    
    public static void BindEcsWorldDebugEngine(DiContainer container)
    {
      container
        .BindInterfacesTo<EcsWorldDebugEngine>()
        .AsSingle()
        .NonLazy();
    }

    public static void BindInputWatcherDebug(DiContainer container)
    {
      container
        .BindInterfacesTo<InputDelayWatcher>()
        .AsSingle()
        .NonLazy();
    }
    
    public static void BindGravityScaleWatcher(DiContainer container)
    {
      container
        .BindInterfacesTo<GravityScaleWatcher>()
        .AsSingle()
        .NonLazy();
    }
  }
}
#endif