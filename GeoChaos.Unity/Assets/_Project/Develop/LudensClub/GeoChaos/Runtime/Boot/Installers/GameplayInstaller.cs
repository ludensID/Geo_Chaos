using System.Collections.Generic;
using System.Linq;
using LudensClub.GeoChaos.Runtime.AI;
using LudensClub.GeoChaos.Runtime.Gameplay;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Enemies;
using LudensClub.GeoChaos.Runtime.Gameplay.Enemies.Lama.Detection;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Collisions;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.Ring;
using LudensClub.GeoChaos.Runtime.Gameplay.Shard;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.BehaviourTrees;
using LudensClub.GeoChaos.Runtime.Infrastructure.Selection;
using LudensClub.GeoChaos.Runtime.Props;
using LudensClub.GeoChaos.Runtime.Props.Shard;
using LudensClub.GeoChaos.Runtime.UI;
using LudensClub.GeoChaos.Runtime.UI.HeroHealth;
using LudensClub.GeoChaos.Runtime.UI.ImmunityDuration;
using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Boot
{
  [AddComponentMenu(ACC.Names.GAMEPLAY_INSTALLER)]
  public class GameplayInstaller : MonoInstaller
  {
    [SerializeField]
    private DashCooldownView _dashCooldownView;

    [SerializeField]
    private ShootCooldownView _shootCooldownView;
    
    [SerializeField]
    private HeroHealthView _healthView;

    [SerializeField]
    private ImmunityDurationView _immunityDurationView;

    [SerializeField]
    private Camera _camera;

    public override void InstallBindings()
    {
      BindInitializingPhase();

      BindNodeStrategyFactory();
      BindBehaviourTreeBuilder();

      BindEnemyTreeCreator();
      BindLamaTreeCreator();

      BindTreeCreatorService();
      
      BindEcsDisposer();
      BindEcsSystemFactory();
      BindInputWorldWrapper();
      BindGameWorldWrapper();
      BindMessageWorldWrapper();
      BindPhysicsWorldWrapper();

      BindSelectionAlgorithmFactory();
      BindRingSelector();
      BindEnemySelector();
      BindAimedLamaSelector();
      
      BindDragForceService();
      BindADControlService();
      BindSpeedForceLoopService();
      BindSpeedForceFactory();
      BindEcsSystemsFactory();
      BindViewFactory();
      BindCollisionFiller();
      BindCollisionService();
      BindSpawnPoints();
      BindShardPool();
      BindShardFactory();
      BindShootService();
      BindCameraService();
      BindFreeFallService();

#if UNITY_EDITOR
      DebugBridge.InstallGameplay(Container);
#endif

      BindEngine();

      BindDashCooldownPresenter();
      BindShootCooldownPresenter();
      BindHeroHealthPresenter();
      BindImmunityDurationPresenter();
    }

    private void BindInitializingPhase()
    {
      Container
        .Bind<IInitializingPhase>()
        .To<InitializingPhase>()
        .AsSingle()
        .NonLazy();
    }

    private void BindImmunityDurationPresenter()
    {
      Container
        .BindInterfacesTo<ImmunityDurationPresenter>()
        .AsSingle()
        .WithArguments(_immunityDurationView);
    }

    private void BindHeroHealthPresenter()
    {
      Container
        .BindInterfacesTo<HeroHealthPresenter>()
        .AsSingle()
        .WithArguments(_healthView);
    }

    private void BindAimedLamaSelector()
    {
      Container
        .BindInterfacesAndSelfTo<AimedLamaSelector>()
        .AsSingle();
    }

    private void BindTreeCreatorService()
    {
      Container
        .Bind<ITreeCreatorService>()
        .To<TreeCreatorService>()
        .AsSingle();
    }

    private void BindLamaTreeCreator()
    {
      Container
        .Bind<IBehaviourTreeCreator>()
        .To<LamaTreeCreator>()
        .AsSingle();
    }

    private void BindEnemyTreeCreator()
    {
      Container
        .Bind<IBehaviourTreeCreator>()
        .To<EnemyTreeCreator>()
        .AsSingle();
    }

    private void BindBehaviourTreeBuilder()
    {
      Container
        .Bind<IBehaviourTreeBuilder>()
        .To<BehaviourTreeBuilder>()
        .AsSingle();
    }

    private void BindNodeStrategyFactory()
    {
      Container
        .Bind<INodeStrategyFactory>()
        .To<NodeStrategyFactory>()
        .AsSingle();
    }

    private void BindFreeFallService()
    {
      Container
        .Bind<IFreeFallService>()
        .To<FreeFallService>()
        .AsSingle();
    }

    private void BindCameraService()
    {
      Container
        .Bind<ICameraService>()
        .To<CameraService>()
        .AsSingle()
        .WithArguments(_camera);
    }

    private void BindShootCooldownPresenter()
    {
      Container
        .BindInterfacesTo<ShootCooldownPresenter>()
        .AsSingle()
        .WithArguments(_shootCooldownView);
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