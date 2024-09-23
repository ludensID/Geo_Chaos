using Zenject;

namespace LudensClub.GeoChaos.Debugging.Boot
{
  public class EditorInstaller : MonoInstaller
  {
    public override void InstallBindings()
    {
      Container
        .Bind<IInitializable>()
        .To<EditorInitializer>()
        .AsSingle();
    }
  }
}