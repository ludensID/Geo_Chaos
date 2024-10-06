using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Windows
{
  public class WindowCloser : IWindowCloser, ITickable
  {
    private readonly InputData _inputData;
    private readonly IWindowManager _windowManager;

    public bool IsCancelledThisFrame { get; private set; }

    public WindowCloser(InputData inputData, IWindowManager windowManager)
    {
      _inputData = inputData;
      _windowManager = windowManager;
    }

    public void Tick()
    {
      IsCancelledThisFrame = _inputData.IsCancel && _windowManager.Current.Id.IsCloseByCancel();
      if (IsCancelledThisFrame)
        _windowManager.Close();
    }
  }
}