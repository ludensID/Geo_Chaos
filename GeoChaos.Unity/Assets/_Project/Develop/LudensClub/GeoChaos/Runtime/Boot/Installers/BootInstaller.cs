using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Boot
{
  [AddComponentMenu(ACC.Names.BOOT_INSTALLER)]
  public class BootInstaller : MonoInstaller
  {
    public override void InstallBindings()
    {
      BindBootInitializer();
    }

    private void BindBootInitializer()
    {
      Container
        .Bind<IInitializable>()
        .To<BootInitializer>()
        .AsSingle();
    }
  }
}