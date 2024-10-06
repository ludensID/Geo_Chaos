using LudensClub.GeoChaos.Runtime.Windows;
using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Boot
{
  [AddComponentMenu(ACC.Names.MENU_INSTALLER)]
  public class MenuInstaller : MonoInstaller
  {
    public override void InstallBindings()
    {
      InstallWindowBindings();
      BindMenuInitializer();
    }

    private void InstallWindowBindings()
    {
      WindowInstaller.Install(Container);
    }

    private void BindMenuInitializer()
    {
      Container
        .Bind<IInitializable>()
        .To<MenuInitializer>()
        .AsSingle();

      Container.BindInitializableExecutionOrder<MenuInitializer>(1);
    }
  }
}