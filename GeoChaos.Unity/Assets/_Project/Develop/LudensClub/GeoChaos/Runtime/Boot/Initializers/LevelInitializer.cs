using Cysharp.Threading.Tasks;
using LudensClub.GeoChaos.Runtime.Infrastructure.StateMachineComponents;
using LudensClub.GeoChaos.Runtime.Windows;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Boot
{
  public class LevelInitializer : IInitializable
  {
    private readonly LevelStateMachine _levelStateMachine;
    private readonly IStateFactory _stateFactory;
    private readonly IWindowManager _windowManager;

    public LevelInitializer(LevelStateMachine levelStateMachine, IStateFactory stateFactory, IWindowManager windowManager)
    {
      _levelStateMachine = levelStateMachine;
      _stateFactory = stateFactory;
      _windowManager = windowManager;
    }
    
    public void Initialize()
    {
      _levelStateMachine.RegisterState(_stateFactory.Create<GameplayLevelState>());
      _levelStateMachine.RegisterState(_stateFactory.Create<PauseLevelState>());
      _levelStateMachine.RegisterState(_stateFactory.Create<RestartLevelState>());

      _levelStateMachine.SwitchState<GameplayLevelState>().Forget();
      
      _windowManager.SetDefaultWindow(WindowType.HUD);
      _windowManager.OpenDefaultWindow();
    }
  }
}