using System.Collections.Generic;
using System.Linq;
using LudensClub.GeoChaos.Runtime.Gameplay;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Props;
using LudensClub.GeoChaos.Runtime.UI;
using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Boot
{
  public class GameplayInstaller : MonoInstaller
  {
    [SerializeField]
    private DashCooldownView _dashCooldownView;

    [SerializeField]
    private EnemyHealthView _enemyHealthView;

    public override void InstallBindings()
    {
      BindEcsSystemFactory();
      BindInputWorldWrapper();
      BindGameWorldWrapper();
      BindMessageWorldWrapper();
      BindEcsSystemsFactory();
      BindViewFactory();
      BindCollisionFiller();
      BindCollisionService();
      BindSpawnPoints();

#if UNITY_EDITOR
      Debugging.DebugBridge.InstallGameplay(Container);
#endif

      BindEngine();

      BindDashCooldownPresenter();
      BindEnemyHealthView();
    }

    private void BindEnemyHealthView()
    {
      Container
        .BindInterfacesTo<EnemyHealthPresenter>()
        .AsSingle()
        .WithArguments(_enemyHealthView);
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