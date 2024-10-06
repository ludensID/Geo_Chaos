using Cysharp.Threading.Tasks;

namespace LudensClub.GeoChaos.Runtime.Persistence
{
  public class PersistenceService : IPersistenceService
  {
    private readonly IGamePersistenceProcessor _gameProcessor;
    private readonly IGamePersistenceProvider _gameProvider;
    private readonly IGamePersistenceLoader _gameLoader;
    private readonly ISettingsPersistenceLoader _settingsLoader;
    private readonly ISettingsPersistenceProvider _settingsProvider;

    public GamePersistence GamePersistence
    {
      get => _gameProvider.Persistence;
      set => _gameProvider.Persistence = value;
    }

    public SettingsPersistence SettingsPersistence
    {
      get => _settingsProvider.Persistence;
      set => _settingsProvider.Persistence = value;
    }

    public PersistenceService(IGamePersistenceProcessor gameProcessor,
      IGamePersistenceProvider gameProvider,
      IGamePersistenceLoader gameLoader,
      ISettingsPersistenceLoader settingsLoader,
      ISettingsPersistenceProvider settingsProvider)
    {
      _gameProcessor = gameProcessor;
      _gameProvider = gameProvider;
      _gameLoader = gameLoader;
      _settingsLoader = settingsLoader;
      _settingsProvider = settingsProvider;
    }

    public async UniTask LoadGameAsync()
    {
      await _gameLoader.LoadAsync();
      GamePersistence ??= new GamePersistence();
    }

    public void ResetGamePersistence()
    {
      GamePersistence = new GamePersistence();
    }

    public async UniTask LoadSettingsAsync()
    {
      await _settingsLoader.LoadAsync();
      SettingsPersistence ??= new SettingsPersistence();
    }

    public GamePersistence GetDirtyGamePersistence()
    {
      _gameProcessor.SetDirty();
      return GamePersistence;
    }

    public void SaveGame()
    {
      _gameProcessor.SetDirty();
    }

    public UniTask SaveGameDirect()
    {
      return _gameProcessor.SaveDirectAsync();
    }

    public UniTask SaveSettings()
    {
      return _settingsLoader.SaveAsync();
    }
  }
}