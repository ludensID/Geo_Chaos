using LudensClub.GeoChaos.Editor.General;
using LudensClub.GeoChaos.Runtime;
using Zenject;

namespace LudensClub.GeoChaos.Editor.Boot
{
  public class EditorInstaller : Installer<EditorInstaller>
  {
    public override void InstallBindings()
    {
      BindEditorInitializer();

      BindProfilerService();

      BindTypeCache();
    }

    private void BindEditorInitializer()
    {
      Container
        .Bind<IEditorInitializer>()
        .To<EditorInitializer>()
        .AsSingle()
        .NonLazy();
    }

    private void BindProfilerService()
    {
      Container
        .Bind<IProfilerService>()
        .To<ProfilerService>()
        .AsSingle();
    }

    private void BindTypeCache()
    {
      Container
        .Bind<ITypeCache>()
        .To<TypeCache>()
        .AsSingle();
    }
  }
}