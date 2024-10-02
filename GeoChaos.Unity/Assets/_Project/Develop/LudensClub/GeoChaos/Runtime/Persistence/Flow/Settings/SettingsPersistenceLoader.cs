namespace LudensClub.GeoChaos.Runtime.Persistence
{
  public class SettingsPersistenceLoader : PersistenceDataLoader<SettingsPersistence>, ISettingsPersistenceLoader
  { 
    public SettingsPersistenceLoader(ISettingsPersistenceProvider settingsPersistenceProvider,
      IFileHandler fileHandler,
      IPathHandler pathHandler)
      : base(settingsPersistenceProvider, fileHandler, pathHandler.SettingsDataPath)
    {
    }
  }
}