using Zenject;

namespace LudensClub.GeoChaos.Runtime.Persistence
{
  public class PersistenceInstaller : Installer<PersistenceInstaller>
  {
    public override void InstallBindings()
    {
      BindPathHandler();
      BindFileHandler();

      BindGameDataProvider();
      BindGameDataLoader();
      BindGamePersistenceProcessor();

      BindSettingsDataProvider();
      BindSettingsDataLoader();
        
      BindPersistenceService();
    }

    private void BindPathHandler()
    {
      Container
        .Bind<IPathHandler>()
        .To<PathHandler>()
        .AsSingle();
    }

    private void BindFileHandler()
    {
      Container
        .Bind<IFileHandler>()
        .To<FileHandler>()
        .AsSingle();
    }

    private void BindGameDataProvider()
    {
      Container
        .Bind<IGamePersistenceProvider>()
        .To<GamePersistenceProvider>()
        .AsSingle();
    }

    private void BindGameDataLoader()
    {
      Container
        .Bind<IGamePersistenceLoader>()
        .To<GamePersistenceLoader>()
        .AsSingle();
    }

    private void BindGamePersistenceProcessor()
    {
      Container
        .BindInterfacesTo<GamePersistenceProcessor>()
        .AsSingle();
    }

    private void BindSettingsDataProvider()
    {
      Container
        .Bind<ISettingsPersistenceProvider>()
        .To<SettingsPersistenceProvider>()
        .AsSingle();
    }

    private void BindSettingsDataLoader()
    {
      Container
        .Bind<ISettingsPersistenceLoader>()
        .To<SettingsPersistenceLoader>()
        .AsSingle();
    }

    private void BindPersistenceService()
    {
      Container
        .Bind<IPersistenceService>()
        .To<PersistenceService>()
        .AsSingle();
    }
  }
}