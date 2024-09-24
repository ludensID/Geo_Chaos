using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.StateMachine;
using LudensClub.GeoChaos.Runtime.Persistence;
using LudensClub.GeoChaos.Runtime.Windows;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Boot
{
  [AddComponentMenu(ACC.Names.PROJECT_INSTALLER)]
  public class ProjectInstaller : MonoInstaller
  {
    [SerializeField]
    private ConfigProvider _configProvider;

    [SerializeField]
    private PlayerInput _input;

    [SerializeField]
    private EventSystem _eventSystem;

    public override void InstallBindings()
    {
      JsonConvert.DefaultSettings = () => new JsonSerializerSettings
      {
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        Formatting = Formatting.Indented
      };

      BindProjectInitializer();

      BindStateFactory();
      BindGameStateMachine();

      BindEventSystem();
      BindExplicitInitializer();
      BindConfigProvider();
      BindInputConfig();
      BindInputDataProvider();
      BindInputSwitcher();
      BindInputController();
      BindTimerService();
      BindTimerFactory();
      BindCoroutineRunner();
      BindPathHandler();
      BindFileHandler();

      BindPersistenceProvider();
      BindGameDataLoader();
      BindGamePersistenceProcessor();
      BindPersistenceService();

      BindBaseWindowModel();
      BindBaseWindowController();

#if UNITY_EDITOR
      DebugBridge.InstallProject(Container);
#endif
    }

    private void BindBaseWindowModel()
    {
      Container
        .Bind<WindowModel>()
        .AsTransient()
        .CopyIntoDirectSubContainers();
    }

    private void BindBaseWindowController()
    {
      Container
        .Bind<WindowController>()
        .AsTransient()
        .CopyIntoDirectSubContainers();
    }

    private void BindProjectInitializer()
    {
      Container
        .Bind<IInitializable>()
        .To<ProjectInitializer>()
        .AsSingle();
    }

    private void BindGameStateMachine()
    {
      Container
        .Bind<GameStateMachine>()
        .AsSingle();
    }

    private void BindStateFactory()
    {
      Container
        .Bind<IStateFactory>()
        .To<StateFactory>()
        .AsSingle()
        .CopyIntoAllSubContainers();
    }

    private void BindPersistenceService()
    {
      Container
        .Bind<IPersistenceService>()
        .To<PersistenceService>()
        .AsSingle();
    }

    private void BindGamePersistenceProcessor()
    {
      Container
        .BindInterfacesTo<GamePersistenceProcessor>()
        .AsSingle();
    }

    private void BindGameDataLoader()
    {
      Container
        .Bind<IGameDataLoader>()
        .To<GameDataLoader>()
        .AsSingle();
    }

    private void BindFileHandler()
    {
      Container
        .Bind<IFileHandler>()
        .To<FileHandler>()
        .AsSingle();
    }

    private void BindPathHandler()
    {
      Container
        .Bind<IPathHandler>()
        .To<PathHandler>()
        .AsSingle();
    }

    private void BindPersistenceProvider()
    {
      Container
        .Bind<IGameDataProvider>()
        .To<GameDataProvider>()
        .AsSingle();
    }

    private void BindEventSystem()
    {
      Container
        .BindInstance(_eventSystem)
        .AsSingle();
    }

    private void BindExplicitInitializer()
    {
      Container
        .Bind<IExplicitInitializer>()
        .To<ExplicitInitializer>()
        .AsSingle()
        .CopyIntoAllSubContainers();
    }

    private void BindInputSwitcher()
    {
      Container
        .Bind<IInputSwitcher>()
        .To<InputSwitcher>()
        .AsSingle();
    }

    private void BindInputConfig()
    {
      Container
        .BindInterfacesAndSelfTo<InputConfig>()
        .AsSingle();
    }

    private void BindTimerFactory()
    {
      Container
        .Bind<ITimerFactory>()
        .To<TimerFactory>()
        .AsSingle();
    }

    private void BindConfigProvider()
    {
      Container
        .Bind<IConfigProvider>()
        .FromInstance(_configProvider)
        .AsSingle();
    }

    private void BindInputDataProvider()
    {
      Container
        .Bind<InputData>()
        .AsSingle();
    }

    private void BindInputController()
    {
      Container
        .BindInterfacesTo<InputController>()
        .AsSingle()
        .WithArguments(_input);
    }

    private void BindTimerService()
    {
      Container
        .BindInterfacesTo<TimerService>()
        .AsSingle();
    }

    private void BindCoroutineRunner()
    {
      Container
        .Bind<ICoroutineRunner>()
        .To<CoroutineRunner>()
        .FromNewComponentOnNewGameObject()
        .WithGameObjectName("CoroutineRunner")
        .UnderTransform(transform)
        .AsSingle();
    }
  }
}