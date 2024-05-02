using LudensClub.GeoChaos.Debugging.Monitoring;
using LudensClub.GeoChaos.Debugging.Watchers;
using Zenject;

namespace LudensClub.GeoChaos.Debugging
{
  public static class DebugInstaller
  {
    public static DiContainer Container { get; private set; }

    public static void InstallProject(DiContainer container)
    {
      Container = container;

      InstallProjectInternal();

      Container = null;
    }

    private static void InstallProjectInternal()
    {
      BindInputDebug();
    }

    public static void InstallGameplay(DiContainer container)
    {
      Container = container;

      InstallGameplayInternal();

      Container = null;
    }

    private static void InstallGameplayInternal()
    {
      BindInputWatcherDebug();
      BindGravityScaleWatcher();
      BindHookInterruptionWatcher();
      BindDashAvailableWatcher();
      BindAttackAvailableWatcher();
      BindHookAvailableWatcher();
      BindDragForceAvailableWatcher();
      BindControllableWatcher();

      BindGlobalWatcher();

      BindEcsUniverseViewFactory();
      BindEcsWorldPresenterFactory();
      BindEcsWorldViewFactory();
      BindEcsEntityPresenterFactory();
      BindEcsEntityViewFactory();
      BindEcsUniversePresenter();
    }

    private static void BindControllableWatcher()
    {
      Container
        .BindInterfacesTo<ControllableWatcher>()
        .AsSingle();
    }

    private static void BindDragForceAvailableWatcher()
    {
      Container
        .BindInterfacesTo<DragForceAvailableWatcher>()
        .AsSingle();
    }

    private static void BindHookAvailableWatcher()
    {
      Container
        .BindInterfacesTo<HookAvailableWatcher>()
        .AsSingle();
    }

    private static void BindAttackAvailableWatcher()
    {
      Container
        .BindInterfacesTo<AttackAvailableWatcher>()
        .AsSingle();
    }

    private static void BindDashAvailableWatcher()
    {
      Container
        .BindInterfacesTo<DashAvailableWatcher>()
        .AsSingle();
    }

    private static void BindGlobalWatcher()
    {
      Container
        .BindInterfacesTo<GlobalWatcher>()
        .AsSingle()
        .NonLazy();
    }

    private static void BindHookInterruptionWatcher()
    {
      Container
        .BindInterfacesTo<HookInterruptionWatcher>()
        .AsSingle()
        .NonLazy();
    }

    public static void BindInputDebug()
    {
      Container
        .Bind<InputDebug>()
        .FromNewComponentOnNewGameObject()
        .WithGameObjectName($"[{nameof(InputDebug)}]")
        .AsSingle()
        .NonLazy();
    }

    public static void BindInputWatcherDebug()
    {
      Container
        .BindInterfacesTo<InputDelayWatcher>()
        .AsSingle()
        .NonLazy();
    }

    public static void BindGravityScaleWatcher()
    {
      Container
        .BindInterfacesTo<GravityScaleWatcher>()
        .AsSingle()
        .NonLazy();
    }

    public static void BindEcsUniverseViewFactory()
    {
      Container
        .Bind<IEcsUniverseViewFactory>()
        .To<EcsUniverseViewFactory>()
        .AsSingle();
    }

    public static void BindEcsWorldPresenterFactory()
    {
      Container
        .Bind<IEcsWorldPresenterFactory>()
        .To<EcsWorldPresenterFactory>()
        .AsSingle();
    }

    public static void BindEcsWorldViewFactory()
    {
      Container
        .Bind<IEcsWorldViewFactory>()
        .To<EcsWorldViewFactory>()
        .AsSingle();
    }

    public static void BindEcsEntityPresenterFactory()
    {
      Container
        .Bind<IEcsEntityPresenterFactory>()
        .To<EcsEntityPresenterFactory>()
        .AsSingle();
    }

    public static void BindEcsEntityViewFactory()
    {
      Container
        .Bind<IEcsEntityViewFactory>()
        .To<EcsEntityViewFactory>()
        .AsSingle();
    }

    public static void BindEcsUniversePresenter()
    {
      Container
        .BindInterfacesTo<EcsUniversePresenter>()
        .AsSingle();
    }
  }
}