using System;
using Cysharp.Threading.Tasks;
using LudensClub.GeoChaos.Runtime.Infrastructure.StateMachineComponents;
using LudensClub.GeoChaos.Runtime.Utils;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Windows
{
  public class WindowPauseHandler : IInitializable, IDisposable
  {
    private readonly IWindowManager _windowManager;
    private readonly LevelStateMachine _levelStateMachine;

    public WindowPauseHandler(IWindowManager windowManager, LevelStateMachine levelStateMachine)
    {
      _windowManager = windowManager;
      _levelStateMachine = levelStateMachine;
    }

    public void Initialize()
    {
      foreach (var window in _windowManager.Windows)
      {
        if (window.Id.IsReactiveOnPause())
        {
          window.OnBeforeOpen += PauseGame;
          window.OnClosed += UnpauseGame;
        }
      }
    }

    public void Dispose()
    {
      foreach (var window in _windowManager.Windows)
      {
        if (window.Id.IsReactiveOnPause())
        {
          window.OnBeforeOpen -= PauseGame;
          window.OnClosed -= UnpauseGame;
        }
      }
    }

    private void PauseGame()
    {
      _levelStateMachine.SwitchState<PauseLevelState>().Forget();
    }

    private void UnpauseGame()
    {
      _levelStateMachine.SwitchState<GameplayLevelState>().Forget();
    }
  }
}