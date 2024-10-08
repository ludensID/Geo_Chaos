using LudensClub.GeoChaos.Editor.Monitoring.Component;
using LudensClub.GeoChaos.Editor.Monitoring.Entity;
using LudensClub.GeoChaos.Editor.Monitoring.Sorting;
using LudensClub.GeoChaos.Editor.Monitoring.Universe;
using LudensClub.GeoChaos.Editor.Monitoring.World;
using LudensClub.GeoChaos.Editor.Watchers;
using Zenject;

namespace LudensClub.GeoChaos.Editor.Boot
{
  public class DebugLevelInstaller : Installer<DebugLevelInstaller>
  {
    public override void InstallBindings()
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

    private void BindEcsComponentSorter()
    {
      Container
        .Bind<IEcsComponentSorter>()
        .To<EcsComponentSorter>()
        .AsSingle();
    }

    private void BindEcsComponentViewFactory()
    {
      Container
        .Bind<IEcsComponentViewFactory>()
        .To<EcsComponentViewFactory>()
        .AsSingle();
    }

    private void BindBumpAvailableWatcher()
    {
      Container
        .BindInterfacesTo<BumpAvailableWatcher>()
        .AsSingle();
    }

    private void BindAimAvailableWatcher()
    {
      Container
        .BindInterfacesTo<AimAvailableWatcher>()
        .AsSingle();
    }

    private void BindShootAvailableWatcher()
    {
      Container
        .BindInterfacesTo<ShootAvailableWatcher>()
        .AsSingle();
    }

    private void BindControllableWatcher()
    {
      Container
        .BindInterfacesTo<ControllableWatcher>()
        .AsSingle();
    }

    private void BindDragForceAvailableWatcher()
    {
      Container
        .BindInterfacesTo<DragForceAvailableWatcher>()
        .AsSingle();
    }

    private void BindHookAvailableWatcher()
    {
      Container
        .BindInterfacesTo<HookAvailableWatcher>()
        .AsSingle();
    }

    private void BindAttackAvailableWatcher()
    {
      Container
        .BindInterfacesTo<AttackAvailableWatcher>()
        .AsSingle();
    }

    private void BindDashAvailableWatcher()
    {
      Container
        .BindInterfacesTo<DashAvailableWatcher>()
        .AsSingle();
    }

    private void BindGlobalWatcher()
    {
      Container
        .BindInterfacesTo<GlobalWatcher>()
        .AsSingle()
        .NonLazy();
    }

    private void BindHookInterruptionWatcher()
    {
      Container
        .BindInterfacesTo<HookInterruptionWatcher>()
        .AsSingle()
        .NonLazy();
    }

    private void BindInputWatcherDebug()
    {
      Container
        .BindInterfacesTo<InputDelayWatcher>()
        .AsSingle()
        .NonLazy();
    }

    private void BindGravityScaleWatcher()
    {
      Container
        .BindInterfacesTo<GravityScaleWatcher>()
        .AsSingle()
        .NonLazy();
    }

    private void BindEcsUniverseViewFactory()
    {
      Container
        .Bind<IEcsUniverseViewFactory>()
        .To<EcsUniverseViewFactory>()
        .AsSingle();
    }

    private void BindEcsWorldPresenterFactory()
    {
      Container
        .Bind<IEcsWorldPresenterFactory>()
        .To<EcsWorldPresenterFactory>()
        .AsSingle();
    }

    private void BindEcsWorldViewFactory()
    {
      Container
        .Bind<IEcsWorldViewFactory>()
        .To<EcsWorldViewFactory>()
        .AsSingle();
    }

    private void BindEcsEntityPresenterFactory()
    {
      Container
        .Bind<IEcsEntityPresenterFactory>()
        .To<EcsEntityPresenterFactory>()
        .AsSingle();
    }

    private void BindEcsEntityViewFactory()
    {
      Container
        .Bind<IEcsEntityViewFactory>()
        .To<EcsEntityViewFactory>()
        .AsSingle();
    }

    private void BindEcsUniversePresenter()
    {
      Container
        .BindInterfacesTo<EcsUniversePresenter>()
        .AsSingle();
    }
  }
}