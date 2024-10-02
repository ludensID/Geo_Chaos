using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.SceneLoading;
using LudensClub.GeoChaos.Runtime.Infrastructure.StateMachineComponents;
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
        
      InstallPersistence();

      BindBaseWindowModel();
      BindBaseWindowController();

      InstallCurtain();

      BindSceneLoader();
        
#if UNITY_EDITOR
      DebugBridge.InstallProject(Container);
#endif
    }

    private void InstallPersistence()
    {
      PersistenceInstaller.Install(Container);
    }

    private void BindSceneLoader()
    {
      Container
        .Bind<ISceneLoader>()
        .To<SceneLoader>()
        .AsSingle();
    }

    private void InstallCurtain()
    {
      CurtainInstaller.Install(Container);
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