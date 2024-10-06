using LudensClub.GeoChaos.Runtime.Windows.Simple;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Windows
{
  public class WindowInstaller : Installer<WindowInstaller>
  {
    public override void InstallBindings()
    {
      BindWindowManager();
      BindWindowCloser();
      BindNavigationElementSetter();
    }

    private void BindWindowManager()
    {
      Container
        .Bind<IWindowManager>()
        .To<WindowManager>()
        .AsSingle();
    }

    private void BindWindowCloser()
    {
      Container
        .BindInterfacesTo<WindowCloser>()
        .AsSingle();
    }

    private void BindNavigationElementSetter()
    {
      Container
        .Bind<INavigationElementSetter>()
        .To<NavigationElementSetter>()
        .AsTransient();
    }
  }
}