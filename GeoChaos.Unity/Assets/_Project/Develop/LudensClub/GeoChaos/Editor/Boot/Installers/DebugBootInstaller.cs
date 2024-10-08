using LudensClub.GeoChaos.Runtime.Boot;
using Zenject;

namespace LudensClub.GeoChaos.Editor.Boot
{
  public class DebugBootInstaller : Installer<DebugBootInstaller>
  {
    public override void InstallBindings()
    {
      RebindBootInitializer();
    }

    private void RebindBootInitializer()
    {
      Container.Unbind<IInitializable, BootInitializer>();
        
      Container
        .Bind<IInitializable>()
        .To<DebugBootInitializer>()
        .AsSingle();
    }
  }
}