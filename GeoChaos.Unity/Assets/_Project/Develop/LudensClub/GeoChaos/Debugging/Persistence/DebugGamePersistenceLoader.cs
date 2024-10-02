using Cysharp.Threading.Tasks;
using LudensClub.GeoChaos.Runtime;
using LudensClub.GeoChaos.Runtime.Persistence;
using Zenject;

namespace LudensClub.GeoChaos.Debugging.Persistence
{
  public class DebugGamePersistenceLoader : IGamePersistenceLoader
  {
    private readonly IGamePersistenceProvider _gamePersistenceProvider;
    private readonly GamePersistenceLoader _gamePersistenceLoader;
    private readonly IPersistencePreferencesLoader _preferencesLoader;
    private readonly IPersistencePreferencesProvider _preferencesProvider;

    public DebugGamePersistenceLoader(IInstantiator instantiator, IGamePersistenceProvider gamePersistenceProvider)
    {
      _gamePersistenceProvider = gamePersistenceProvider;
      _gamePersistenceLoader = instantiator.Instantiate<GamePersistenceLoader>();
      _preferencesLoader = EditorMediator.Context.Container.Resolve<IPersistencePreferencesLoader>();
      _preferencesProvider = EditorMediator.Context.Container.Resolve<IPersistencePreferencesProvider>();
    }

    public async UniTask LoadAsync()
    {
      await _gamePersistenceLoader.LoadAsync();
      if (_preferencesProvider.Preferences.EnableSaving)
      {
        if (_preferencesProvider.Preferences.EnableSync)
        {
          _gamePersistenceProvider.Persistence = _preferencesProvider.Preferences.GamePersistence;
          _preferencesProvider.Preferences.GamePersistence = _gamePersistenceProvider.Persistence;
        }
      }
      else
      {
        _gamePersistenceProvider.Persistence = null;
      }
    }

    public UniTask SaveAsync()
    {
      if (_preferencesProvider.Preferences.EnableSaving)
      {
        _preferencesLoader.SaveToJson();
        return _gamePersistenceLoader.SaveAsync();
      }
      
      return UniTask.CompletedTask;
    }
  }
}