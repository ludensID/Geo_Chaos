using System.Collections.Generic;
using System.Linq;
using LudensClub.GeoChaos.Runtime.Gameplay;
using LudensClub.GeoChaos.Runtime.Gameplay.Enemy;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Collisions;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.Ring;
using LudensClub.GeoChaos.Runtime.Gameplay.Shard;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Selection;
using LudensClub.GeoChaos.Runtime.Props;
using LudensClub.GeoChaos.Runtime.Props.Ring;
using LudensClub.GeoChaos.Runtime.Props.Shard;
using LudensClub.GeoChaos.Runtime.UI;
using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Boot
{
  public class GameplayInstaller : MonoInstaller
  {
    [SerializeField]
    private DashCooldownView _dashCooldownView;

    public override void InstallBindings()
    {
      BindEcsDisposer();
      BindEcsSystemFactory();
      BindInputWorldWrapper();
      BindGameWorldWrapper();
      BindMessageWorldWrapper();
      BindPhysicsWorldWrapper();

      BindSelectionAlgorithmFactory();
      BindRingSelector();
      BindEnemySelector();
      
      BindDragForceService();
      BindADControlService();
      BindSpeedForceLoopService();
      BindSpeedForceFactory();
      BindEcsSystemsFactory();
      BindViewFactory();
      BindCollisionFiller();
      BindCollisionService();
      BindSpawnPoints();
      BindRingViews();
      BindShardPool();
      BindShardFactory();
      BindShootService();

#if UNITY_EDITOR
      Debugging.DebugBridge.InstallGameplay(Container);
#endif

      BindEngine();

      BindDashCooldownPresenter();
    }

    private void BindShootService()
    {
      Container
        .Bind<IShootService>()
        .To<ShootService>()
        .AsSingle();
    }

    private void BindEnemySelector()
    {
      Container
        .BindInterfacesAndSelfTo<DamagableEntitySelector>()
        .AsSingle();
    }

    private void BindRingSelector()
    {
      Container
        .BindInterfacesAndSelfTo<RingSelector>()
        .AsSingle();
    }

    private void BindSelectionAlgorithmFactory()
    {
      Container
        .Bind<ISelectionAlgorithmFactory>()
        .To<SelectionAlgorithmFactory>()
        .AsSingle();
    }

    private void BindShardFactory()
    {
      Container
        .Bind<IShardFactory>()
        .To<ShardFactory>()
        .AsSingle();
    }

    private void BindShardPool()
    {
      Container
        .BindInterfacesTo<ShardPool>()
        .AsSingle();
    }

    private void BindADControlService()
    {
      Container
        .Bind<IADControlService>()
        .To<ADControlService>()
        .AsSingle();
    }

    private void BindDragForceService()
    {
      Container
        .Bind<IDragForceService>()
        .To<DragForceService>()
        .AsSingle();
    }

    private void BindSpeedForceLoopService()
    {
      Container
        .Bind<ISpeedForceLoopService>()
        .To<SpeedForceLoopService>()
        .AsSingle();
    }

    private void BindSpeedForceFactory()
    {
      Container
        .Bind<ISpeedForceFactory>()
        .To<SpeedForceFactory>()
        .AsSingle();
    }

    private void BindPhysicsWorldWrapper()
    {
      Container
        .BindInterfacesAndSelfTo<PhysicsWorldWrapper>()
        .AsSingle();
    }

    private void BindEcsDisposer()
    {
      Container
        .BindInterfacesTo<EcsDisposer>()
        .AsSingle();
    }

    private void BindRingViews()
    {
      List<RingView> rings = FindObjectsByType<RingView>(FindObjectsInactive.Exclude, FindObjectsSortMode.None).ToList();
      Container
        .Bind<List<RingView>>()
        .FromInstance(rings)
        .AsSingle();
    }

    private void BindCollisionService()
    {
      Container
        .Bind<ICollisionService>()
        .To<CollisionService>()
        .AsSingle();
    }

    private void BindEcsSystemsFactory()
    {
      Container
        .Bind<IEcsSystemsFactory>()
        .To<EcsSystemsFactory>()
        .AsSingle();
    }

    private void BindDashCooldownPresenter()
    {
      Container
        .BindInterfacesTo<DashCooldownPresenter>()
        .AsSingle()
        .WithArguments(_dashCooldownView);
    }

    private void BindViewFactory()
    {
      Container
        .Bind<IViewFactory>()
        .To<ViewFactory>()
        .AsSingle();
    }

    private void BindSpawnPoints()
    {
      var spawns = FindObjectsByType<SpawnPoint>(FindObjectsInactive.Exclude, FindObjectsSortMode.None).ToList();
      Container
        .Bind<List<SpawnPoint>>()
        .FromInstance(spawns)
        .AsSingle();
    }

    private void BindMessageWorldWrapper()
    {
      Container
        .BindInterfacesAndSelfTo<MessageWorldWrapper>()
        .AsSingle();
    }

    private void BindCollisionFiller()
    {
      Container
        .Bind<ICollisionFiller>()
        .To<CollisionFiller>()
        .AsSingle();
    }

    private void BindInputWorldWrapper()
    {
      Container
        .BindInterfacesAndSelfTo<InputWorldWrapper>()
        .AsSingle();
    }

    private void BindGameWorldWrapper()
    {
      Container
        .BindInterfacesAndSelfTo<GameWorldWrapper>()
        .AsSingle();
    }

    private void BindEcsSystemFactory()
    {
      Container
        .Bind<IEcsSystemFactory>()
        .To<EcsSystemFactory>()
        .AsSingle();
    }

    private void BindEngine()
    {
      Container
        .BindInterfacesTo<Engine>()
        .AsSingle();
    }
  }
}