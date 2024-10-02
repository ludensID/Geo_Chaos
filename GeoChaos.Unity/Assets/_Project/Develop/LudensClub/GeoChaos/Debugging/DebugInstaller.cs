using LudensClub.GeoChaos.Debugging.Monitoring;
using LudensClub.GeoChaos.Debugging.Monitoring.Sorting;
using LudensClub.GeoChaos.Debugging.Persistence;
using LudensClub.GeoChaos.Debugging.Watchers;
using LudensClub.GeoChaos.Runtime.Persistence;
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
      RebindGameDataLoader();
    }

    private static void RebindGameDataLoader()
    {
      Container
        .Rebind<IGamePersistenceLoader>()
        .To<DebugGamePersistenceLoader>()
        .AsSingle();
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
      BindShootAvailableWatcher();
      BindAimAvailableWatcher();
      BindBumpAvailableWatcher();

      BindGlobalWatcher();

      BindEcsComponentSorter();
      BindEcsComponentViewFactory();

      BindEcsUniverseViewFactory();
      BindEcsWorldPresenterFactory();
      BindEcsWorldViewFactory();
      BindEcsEntityPresenterFactory();
      BindEcsEntityViewFactory();
      BindEcsUniversePresenter();
    }

    private static void BindEcsComponentSorter()
    {
      Container
        .Bind<IEcsComponentSorter>()
        .To<EcsComponentSorter>()
        .AsSingle();
    }

    private static void BindEcsComponentViewFactory()
    {
      Container
        .Bind<IEcsComponentViewFactory>()
        .To<EcsComponentViewFactory>()
        .AsSingle();
    }

    private static void BindBumpAvailableWatcher()
    {
      Container
        .BindInterfacesTo<BumpAvailableWatcher>()
        .AsSingle();
    }

    private static void BindAimAvailableWatcher()
    {
      Container
        .BindInterfacesTo<AimAvailableWatcher>()
        .AsSingle();
    }

    private static void BindShootAvailableWatcher()
    {
      Container
        .BindInterfacesTo<ShootAvailableWatcher>()
        .AsSingle();
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

    private static void BindInputDebug()
    {
      Container
        .Bind<InputDebug>()
        .FromNewComponentOnNewGameObject()
        .WithGameObjectName($"[{nameof(InputDebug)}]")
        .AsSingle()
        .NonLazy();
    }

    private static void BindInputWatcherDebug()
    {
      Container
        .BindInterfacesTo<InputDelayWatcher>()
        .AsSingle()
        .NonLazy();
    }

    private static void BindGravityScaleWatcher()
    {
      Container
        .BindInterfacesTo<GravityScaleWatcher>()
        .AsSingle()
        .NonLazy();
    }

    private static void BindEcsUniverseViewFactory()
    {
      Container
        .Bind<IEcsUniverseViewFactory>()
        .To<EcsUniverseViewFactory>()
        .AsSingle();
    }

    private static void BindEcsWorldPresenterFactory()
    {
      Container
        .Bind<IEcsWorldPresenterFactory>()
        .To<EcsWorldPresenterFactory>()
        .AsSingle();
    }

    private static void BindEcsWorldViewFactory()
    {
      Container
        .Bind<IEcsWorldViewFactory>()
        .To<EcsWorldViewFactory>()
        .AsSingle();
    }

    private static void BindEcsEntityPresenterFactory()
    {
      Container
        .Bind<IEcsEntityPresenterFactory>()
        .To<EcsEntityPresenterFactory>()
        .AsSingle();
    }

    private static void BindEcsEntityViewFactory()
    {
      Container
        .Bind<IEcsEntityViewFactory>()
        .To<EcsEntityViewFactory>()
        .AsSingle();
    }

    private static void BindEcsUniversePresenter()
    {
      Container
        .BindInterfacesTo<EcsUniversePresenter>()
        .AsSingle();
    }
  }
}