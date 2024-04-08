using System.Collections.Generic;
using System.Linq;
using LudensClub.GeoChaos.Runtime.Gameplay;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Props;
using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Boot
{
  public class GameplayInstaller : MonoInstaller
  {
    public override void InstallBindings()
    {
      BindEcsSystemFactory();
      BindInputWorldWrapper();
      BindGameWorldWrapper();
      BindViewFactory();
      BindMessageWorldWrapper();
      BindCollisionFiller();
      BindSpawnPoints();

#if UNITY_EDITOR
      Debugging.DebugInstaller.BindEcsWorldDebugEngine(Container);
      Debugging.DebugInstaller.BindInputWatcherDebug(Container);
      Debugging.DebugInstaller.BindGravityScaleWatcher(Container);
#endif

      BindEngine();
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