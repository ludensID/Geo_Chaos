using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Windows
{
  public class WindowCloser : ITickable
  {
    private readonly InputData _inputData;
    private readonly IWindowManager _windowManager;

    public WindowCloser(InputData inputData, IWindowManager windowManager)
    {
      _inputData = inputData;
      _windowManager = windowManager;
    }

    public void Tick()
    {
      if(_inputData.IsPause && _windowManager.CurrentWindowNullOrDefault())
      {
        _windowManager.Open(WindowType.Pause);
        return;
      }

      if (_inputData.IsCancel && _windowManager.Current.Id.IsCloseByCancel())
      {
        _windowManager.Close();
      }
    }
  }
}