#if UNITY_EDITOR
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

    public static void BindInputDelayDebug(DiContainer container)
    {
      container
        .BindInterfacesTo<InputDelayDebug>()
        .AsSingle()
        .NonLazy();
    }
  }
}
#endif