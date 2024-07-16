using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;
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
      BindInputDataProvider();
      BindInputController();
      BindTimerService();
      BindTimerFactory();
      BindCoroutineRunner();
      BindGameObjectConverter();

#if UNITY_EDITOR
      DebugBridge.InstallProject(Container);
#endif
    }

    private void BindTimerFactory()
    {
      Container
        .Bind<ITimerFactory>()
        .To<TimerFactory>()
        .AsSingle();
    }

    private void BindGameObjectConverter()
    {
      Container
        .Bind<IGameObjectConverterService>()
        .To<GameObjectConverterService>()
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
        .Bind<IInputDataProvider>()
        .To<InputDataProvider>()
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
        .AsSingle();
    }
  }
}