using Zenject;

namespace LudensClub.GeoChaos.Editor.Boot
{
  public class EditorBootInstaller : MonoInstaller
  {
    public override void InstallBindings()
    {
      Container
        .Bind<IInitializable>()
        .To<EditorBootInitializer>()
        .AsSingle();
    }
  }
}