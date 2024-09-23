using LudensClub.GeoChaos.Debugging.Persistence;
using LudensClub.GeoChaos.Runtime;
using LudensClub.GeoChaos.Runtime.Persistence;
using Zenject;

namespace LudensClub.GeoChaos.Debugging.Boot
{
  public class EditorInitializer : IInitializable
  {
    private readonly IGameDataProvider _gameDataProvider;
    private readonly IPersistencePreferencesProvider _provider;

    public EditorInitializer(IGameDataProvider gameDataProvider)
    {
      _gameDataProvider = gameDataProvider;
      _provider = EditorMediator.Context.Container.Resolve<IPersistencePreferencesProvider>();
    }

    public void Initialize()
    {
      if (_provider.Preferences.EnableSaving && _provider.Preferences.EnableSync)
      {
        _gameDataProvider.Data = _provider.Preferences.GameData;
        _provider.Preferences.GameData = _gameDataProvider.Data;
      }

      PlayModeSceneLoader.LoadCurrentScene();
    }
  }
}