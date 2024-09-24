using LudensClub.GeoChaos.Runtime.Infrastructure;
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
      if (_inputData.Cancel && _windowManager.Current is ICloseHandler)
      {
        _windowManager.Close();
      }
    }
  }
}