using LudensClub.GeoChaos.Runtime.Infrastructure;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Windows
{
  public class PauseWindowSwitcher : ITickable
  {
    private readonly IWindowCloser _windowCloser;
    private readonly InputData _inputData;
    private readonly IWindowManager _windowManager;

    public PauseWindowSwitcher(IWindowCloser windowCloser, InputData inputData, IWindowManager windowManager)
    {
      _windowCloser = windowCloser;
      _inputData = inputData;
      _windowManager = windowManager;
    }
      
    public void Tick()
    {
      if(!_windowCloser.IsCancelledThisFrame && _inputData.IsPause && _windowManager.CurrentWindowNullOrDefault())
        _windowManager.Open(WindowType.Pause);
    }
  }
}