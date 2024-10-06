using System;
using Cysharp.Threading.Tasks;
using LudensClub.GeoChaos.Runtime.Infrastructure.StateMachineComponents;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Windows.Death
{
  public class LevelRestartActivator : IInitializable, IDisposable
  {
    private readonly IWindowManager _windowManager;
    private readonly LevelStateMachine _levelStateMachine;
    private IWindowController _window;

    public LevelRestartActivator(IWindowManager windowManager, LevelStateMachine levelStateMachine)
    {
      _windowManager = windowManager;
      _levelStateMachine = levelStateMachine;
    }

    public void Initialize()
    {
      _window = _windowManager.FindWindowById(WindowType.Death);
      if (_window != null)
        _window.OnClosed += RestartLevel;
    }

    public void Dispose()
    {
      if (_window != null)
        _window.OnClosed -= RestartLevel;
    }

    private void RestartLevel()
    {
      _levelStateMachine.SwitchState<RestartLevelState>().Forget();
    }
  }
}