using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;
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

    public override void InstallBindings()
    {
      BindConfigProvider();
      BindInputConfig();
      BindInputDataProvider();
      BindInputSwitcher();
      BindInputController();
      BindTimerService();
      BindTimerFactory();
      BindCoroutineRunner();

      BindExplicitInitializer();

#if UNITY_EDITOR
      DebugBridge.InstallProject(Container);
#endif
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