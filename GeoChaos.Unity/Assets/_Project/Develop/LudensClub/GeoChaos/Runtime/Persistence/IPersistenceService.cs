using Cysharp.Threading.Tasks;

namespace LudensClub.GeoChaos.Runtime.Persistence
{
  public interface IPersistenceService
  {
    GamePersistence GamePersistence { get; set; }
    SettingsPersistence SettingsPersistence { get; set; }
    UniTask LoadGameAsync();
    GamePersistence GetDirtyGamePersistence();
    void SaveGame();
    UniTask SaveGameDirect();
    UniTask LoadSettingsAsync();
    UniTask SaveSettings();
    void ResetGamePersistence();
  }
}