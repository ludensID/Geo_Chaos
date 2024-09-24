using LudensClub.GeoChaos.Runtime.Infrastructure;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Windows.Simple
{
  public class NavigationElementSetter : INavigationElementSetter, IInitializable
  {
    private readonly IWindowManager _windowManager;
    private SimpleWindowView _view;
    private WindowController _window;

    public NavigationElementSetter(IWindowManager windowManager, IExplicitInitializer initializer)
    {
      _windowManager = windowManager;
      initializer.Add(this);
    }

    public void SetView(SimpleWindowView view)
    {
      _view = view;
    }

    public void Initialize()
    {
      _window = (WindowController) _windowManager.FindWindowById(_view.Id);
      _window.Model.FirstNavigationElement = _view.FirstNavigationElement;
    }
  }
}