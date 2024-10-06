using LudensClub.GeoChaos.Runtime.Windows;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Boot
{
  public class MenuInitializer : IInitializable
  {
    private readonly IWindowManager _windowManager;

    public MenuInitializer(IWindowManager windowManager)
    {
      _windowManager = windowManager;
    }
      
    public void Initialize()
    {
      _windowManager.SetDefaultWindow(WindowType.Menu);
      _windowManager.OpenDefaultWindow();
    }
  }
}