﻿using LudensClub.GeoChaos.Runtime.Gameplay;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Detection;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Detection;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Lama;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Lama.Detection;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Detection;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Shroom;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Shoot;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Environment.GasCloud;
using LudensClub.GeoChaos.Runtime.Gameplay.Environment.Leaf;
using LudensClub.GeoChaos.Runtime.Gameplay.Environment.Ring;
using LudensClub.GeoChaos.Runtime.Gameplay.Environment.Shard;
using LudensClub.GeoChaos.Runtime.Gameplay.Environment.Tongue;
using LudensClub.GeoChaos.Runtime.Gameplay.FlexibleCamera;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Collisions;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.Restart;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.AI;
using LudensClub.GeoChaos.Runtime.Infrastructure.AI.BehaviourTrees;
using LudensClub.GeoChaos.Runtime.Infrastructure.Selection;
using LudensClub.GeoChaos.Runtime.Infrastructure.StateMachineComponents;
using LudensClub.GeoChaos.Runtime.UI;
using LudensClub.GeoChaos.Runtime.UI.HeroHealth;
using LudensClub.GeoChaos.Runtime.UI.HeroHealthShard;
using LudensClub.GeoChaos.Runtime.UI.ImmunityDuration;
using LudensClub.GeoChaos.Runtime.Windows;
using LudensClub.GeoChaos.Runtime.Windows.Checkpoint;
using LudensClub.GeoChaos.Runtime.Windows.Death;
using LudensClub.GeoChaos.Runtime.Windows.Map;
using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Boot
{
  [AddComponentMenu(ACC.Names.LEVEL_INSTALLER)]
  public class LevelInstaller : MonoInstaller
  {
    [SerializeField]
    private Camera _camera;

    public override void InstallBindings()
    {
      BindLevelInitializer();

      BindLevelStateMachine();
      
      InstallWindowBindings();
      BindWindowPauseHandler();
      BindPauseWindowSwitcher();

      BindRestartProcessor();
      BindGameplayPause();
      BindInitializingPhase();

      BindViewFactory();

      BindNodeStrategyFactory();
      BindBehaviourTreeBuilder();

      BindEnemyTreeCreator();
      BindLamaTreeCreator();
      BindLeafySpiritTreeCreator();
      BindFrogTreeCreator();
      BindZombieTreeCreator();
      BindShroomTreeCreator();

      BindTreeCreatorService();

      BindEcsDisposer();
      BindEcsSystemFactory();
      BindInputWorldWrapper();
      BindGameWorldWrapper();
      BindMessageWorldWrapper();
      BindPhysicsWorldWrapper();

      BindEcsSystemsFactory();

      BindCameraProvider();

      BindSelectionAlgorithmFactory();
      BindRingSelector();
      BindEnemySelector();
      BindAimedLamaSelector();
      BindAimedLeafySpiritSelector();
      BindAimInRadiusLeafySpiritSelector();
      BindFrogTargetInViewSelector();
      BindFrogTargetInFrontSelector();
      BindFrogTargetInBackSelector();
      BindTargetInBoundsSelector();

      BindShardPool();
      BindLeafPool();
      BindTonguePool();
      BindGasCloudPool();

      BindEcsRestartService();

      BindDragForceService();
      BindADControlService();
      BindSpeedForceLoopService();
      BindSpeedForceFactory();

      BindCollisionFiller();
      BindCollisionPacker();
      BindCollisionService();

      BindShardFactory();
      BindShootService();
      BindFreeFallService();

      BindHeroBinder();
      BindHeroTransporter();

#if UNITY_EDITOR
      DebugBridge.InstallGameplay(Container);
#endif

      BindEngine();

      BindVirtualCameraModel();
      BindVirtualCameraManager();
      BindHeroRotationInterpolator();
      BindVerticalDampingInterpolator();
      BindMainCameraSyncer();
      BindEdgeOffsetSetter();
      BindVerticalViewShiftSetter();
      BindAimShiftSetter();
      BindVerticalShiftUpdater();
      BindCameraShiftInterpolator();

      BindSaveButtonPresenter();
      BindMoveButtonPresenter();
      BindMapModel();
      BindMapCheckpointButtonPresenter();
      BindMapWindowPresenter();
      BindLevelRestartActivator();

      BindDashCooldownPresenter();
      BindShootCooldownPresenter();
      BindHeroHealthPresenter();
      BindImmunityDurationPresenter();
      BindHeroHealthShardPresenter();

      Container.DefaultParent = new GameObject("Runtime").transform;
    }

    private void BindPauseWindowSwitcher()
    {
      Container
        .BindInterfacesTo<PauseWindowSwitcher>()
        .AsSingle();
    }

    private void BindWindowPauseHandler()
    {
      Container
        .BindInterfacesTo<WindowPauseHandler>()
        .AsSingle();
    }

    private void InstallWindowBindings()
    {
      WindowInstaller.Install(Container);
    }

    private void BindMoveButtonPresenter()
    {
      Container
        .Bind<IMoveButtonPresenter>()
        .To<MoveButtonPresenter>()
        .AsSingle();
    }

    private void BindEcsRestartService()
    {
      Container
        .Bind<IEcsRestartService>()
        .To<EcsRestartService>()
        .AsSingle();
    }

    private void BindRestartProcessor()
    {
      Container
        .Bind<IRestartProcessor>()
        .To<RestartProcessor>()
        .AsSingle();
    }

    private void BindLevelRestartActivator()
    {
      Container
        .BindInterfacesTo<LevelRestartActivator>()
        .AsSingle();
    }

    private void BindLevelInitializer()
    {
      Container
        .Bind<IInitializable>()
        .To<LevelInitializer>()
        .AsSingle();
    }

    private void BindLevelStateMachine()
    {
      Container
        .Bind<LevelStateMachine>()
        .AsSingle();
    }

    private void BindSaveButtonPresenter()
    {
      Container
        .Bind<ISaveButtonPresenter>()
        .To<SaveButtonPresenter>()
        .AsSingle();
    }

    private void BindMapModel()
    {
      Container
        .Bind<MapModel>()
        .AsSingle();
    }

    private void BindMapWindowPresenter()
    {
      Container
        .Bind<IMapNavigationElementSetter>()
        .To<MapNavigationElementSetter>()
        .AsSingle();
    }

    private void BindMapCheckpointButtonPresenter()
    {
      Container
        .Bind<IMapCheckpointButtonPresenter>()
        .To<MapCheckpointButtonPresenter>()
        .AsTransient();
    }

    private void BindHeroTransporter()
    {
      Container
        .BindInterfacesAndSelfTo<HeroTransporter>()
        .AsSingle();
    }

    private void BindAimShiftSetter()
    {
      Container
        .BindInterfacesTo<AimShiftSetter>()
        .AsSingle();
    }

    private void BindVerticalShiftUpdater()
    {
      Container
        .BindInterfacesTo<CameraShiftUpdater>()
        .AsSingle();
    }

    private void BindCameraShiftInterpolator()
    {
      Container
        .BindInterfacesTo<CameraShiftInterpolator>()
        .AsSingle();
    }

    private void BindVerticalViewShiftSetter()
    {
      Container
        .BindInterfacesTo<VerticalViewShiftSetter>()
        .AsSingle();
    }

    private void BindEdgeOffsetSetter()
    {
      Container
        .Bind<IEdgeShiftSetter>()
        .To<EdgeShiftSetter>()
        .AsSingle();
    }

    private void BindMainCameraSyncer()
    {
      Container
        .BindInterfacesTo<MainCameraSyncer>()
        .AsSingle();
    }

    private void BindVirtualCameraModel()
    {
      Container
        .Bind<VirtualCameraModel>()
        .AsSingle();
    }

    private void BindVirtualCameraManager()
    {
      Container
        .BindInterfacesTo<VirtualCameraManager>()
        .AsSingle();
    }

    private void BindVerticalDampingInterpolator()
    {
      Container
        .BindInterfacesTo<VerticalDampingInterpolator>()
        .AsSingle();
    }

    private void BindHeroRotationInterpolator()
    {
      Container
        .BindInterfacesTo<HeroRotationInterpolator>()
        .AsSingle();
    }

    private void BindHeroBinder()
    {
      Container
        .BindInterfacesTo<HeroHolder>()
        .AsSingle();
    }

    private void BindGasCloudPool()
    {
      Container
        .BindInterfacesAndSelfTo<GasCloudPool>()
        .AsSingle();
    }

    private void BindShroomTreeCreator()
    {
      Container
        .Bind<IBehaviourTreeCreator>()
        .To<ShroomTreeCreator>()
        .AsSingle();
    }

    private void BindTargetInBoundsSelector()
    {
      Container
        .BindInterfacesAndSelfTo<TargetInBoundsSelector>()
        .AsSingle();
    }

    private void BindZombieTreeCreator()
    {
      Container
        .Bind<IBehaviourTreeCreator>()
        .To<ZombieTreeCreator>()
        .AsSingle();
    }

    private void BindTonguePool()
    {
      Container
        .BindInterfacesAndSelfTo<TonguePool>()
        .AsSingle();
    }

    private void BindFrogTargetInBackSelector()
    {
      Container
        .BindInterfacesAndSelfTo<FrogTargetInBackSelector>()
        .AsSingle();
    }

    private void BindFrogTargetInFrontSelector()
    {
      Container
        .BindInterfacesAndSelfTo<FrogTargetInFrontSelector>()
        .AsSingle();
    }

    private void BindFrogTargetInViewSelector()
    {
      Container
        .BindInterfacesAndSelfTo<FrogTargetInViewSelector>()
        .AsSingle();
    }

    private void BindFrogTreeCreator()
    {
      Container
        .Bind<IBehaviourTreeCreator>()
        .To<FrogTreeCreator>()
        .AsSingle();
    }

    private void BindCameraProvider()
    {
      Container
        .Bind<ICameraProvider>()
        .To<CameraProvider>()
        .AsSingle()
        .WithArguments(_camera);
    }

    private void BindCollisionPacker()
    {
      Container
        .Bind<ICollisionPacker>()
        .To<CollisionPacker>()
        .AsSingle();
    }

    private void BindAimInRadiusLeafySpiritSelector()
    {
      Container
        .BindInterfacesAndSelfTo<AimInRadiusLeafySpiritSelector>()
        .AsSingle();
    }

    private void BindAimedLeafySpiritSelector()
    {
      Container
        .BindInterfacesAndSelfTo<AimedLeafySpiritSelector>()
        .AsSingle();
    }

    private void BindLeafySpiritTreeCreator()
    {
      Container
        .Bind<IBehaviourTreeCreator>()
        .To<LeafySpiritTreeCreator>()
        .AsSingle();
    }

    private void BindLeafPool()
    {
      Container
        .BindInterfacesAndSelfTo<LeafPool>()
        .AsSingle();
    }

    private void BindGameplayPause()
    {
      Container
        .Bind<IGameplayPause>()
        .To<GameplayPause>()
        .AsSingle();
    }

    private void BindHeroHealthShardPresenter()
    {
      Container
        .Bind<IHeroHealthShardPresenter>()
        .To<HeroHealthShardPresenter>()
        .AsSingle();
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
        .Bind<IImmunityDurationPresenter>()
        .To<ImmunityDurationPresenter>()
        .AsSingle();
    }

    private void BindHeroHealthPresenter()
    {
      Container
        .Bind<IHeroHealthPresenter>()
        .To<HeroHealthPresenter>()
        .AsSingle();
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

    private void BindShootCooldownPresenter()
    {
      Container
        .Bind<IShootCooldownPresenter>()
        .To<ShootCooldownPresenter>()
        .AsSingle();
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
        .BindInterfacesAndSelfTo<ShardPool>()
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
        .Bind<IDashCooldownPresenter>()
        .To<DashCooldownPresenter>()
        .AsSingle();
    }

    private void BindViewFactory()
    {
      Container
        .Bind<IViewFactory>()
        .To<ViewFactory>()
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