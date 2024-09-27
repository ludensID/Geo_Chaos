using Cysharp.Threading.Tasks;
using LudensClub.GeoChaos.Runtime;
using LudensClub.GeoChaos.Runtime.Persistence;
using Zenject;

namespace LudensClub.GeoChaos.Debugging.Persistence
{
  public class DebugGameDataLoader : IGameDataLoader
  {
    private readonly IGameDataProvider _gameDataProvider;
    private readonly GameDataLoader _gameDataLoader;
    private readonly IPersistencePreferencesLoader _preferencesLoader;
    private readonly IPersistencePreferencesProvider _preferencesProvider;

    public DebugGameDataLoader(IInstantiator instantiator, IGameDataProvider gameDataProvider)
    {
      _gameDataProvider = gameDataProvider;
      _gameDataLoader = instantiator.Instantiate<GameDataLoader>();
      _preferencesLoader = EditorMediator.Context.Container.Resolve<IPersistencePreferencesLoader>();
      _preferencesProvider = EditorMediator.Context.Container.Resolve<IPersistencePreferencesProvider>();
    }

    public async UniTask LoadAsync()
    {
      await _gameDataLoader.LoadAsync();
      if (_preferencesProvider.Preferences.EnableSaving)
      {
        if (_preferencesProvider.Preferences.EnableSync)
        {
          _gameDataProvider.Data = _preferencesProvider.Preferences.GameData;
          _preferencesProvider.Preferences.GameData = _gameDataProvider.Data;
        }
      }
      else
      {
        _gameDataProvider.Data = null;
      }
    }

    public UniTask SaveAsync()
    {
      if (_preferencesProvider.Preferences.EnableSaving)
      {
        _preferencesLoader.SaveToJson();
        return _gameDataLoader.SaveAsync();
      }
      
      return UniTask.CompletedTask;
    }
  }
}