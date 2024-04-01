using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Debugging;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Boot
{
  public class ProjectInstaller : MonoInstaller
  {
    [SerializeField] private ConfigProvider _configProvider;
    [SerializeField] private PlayerInput _input;

    public override void InstallBindings()
    {
      BindConfigProvider();
      BindInputDataProvider();
      BindInputController();
      BindTimerService();
      BindCoroutineRunner();
      BindGameOjbjectConverter();

#if UNITY_EDITOR
      BindInputDebug();
#endif
    }

    private void BindGameOjbjectConverter()
    {
      Container
        .Bind<IGameObjectConverter>()
        .To<GameObjectConverter>()
        .AsSingle();
    }

#if UNITY_EDITOR
    private void BindInputDebug()
    {
      Container
        .Bind<InputDebug>()
        .FromNewComponentOnNewGameObject()
        .WithGameObjectName($"[{nameof(InputDebug)}]")
        .AsSingle()
        .NonLazy();
    }
#endif

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