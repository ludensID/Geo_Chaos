using Zenject;

namespace LudensClub.GeoChaos.Debugging.Persistence
{
  public class PersistenceEditorInstaller : Installer<PersistenceEditorInstaller>
  {
    public override void InstallBindings()
    {
      BindPersistencePreferencesProvider();
      BindPersistencePreferencesLoader();
    }

    private void BindPersistencePreferencesProvider()
    {
      Container
        .Bind<IPersistencePreferencesProvider>()
        .To<PersistencePreferencesProvider>()
        .AsSingle();
    }

    private void BindPersistencePreferencesLoader()
    {
      Container
        .BindInterfacesTo<PersistencePreferencesLoader>()
        .AsSingle();
    }
  }
}