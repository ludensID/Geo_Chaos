using Cysharp.Threading.Tasks;
using LudensClub.GeoChaos.Runtime.Persistence;
using Zenject;

namespace LudensClub.GeoChaos.Debugging.Persistence
{
  public class DebugGamePersistenceLoader : IGamePersistenceLoader
  {
    private readonly IGamePersistenceProvider _gamePersistenceProvider;
    private readonly GamePersistenceLoader _gamePersistenceLoader;

    public DebugGamePersistenceLoader(IInstantiator instantiator, IGamePersistenceProvider gamePersistenceProvider)
    {
      _gamePersistenceProvider = gamePersistenceProvider;
      _gamePersistenceLoader = instantiator.Instantiate<GamePersistenceLoader>();
    }

    public async UniTask LoadAsync()
    {
      await _gamePersistenceLoader.LoadAsync();
      if (PersistencePreferences.instance.EnableSaving)
      {
        if (PersistencePreferences.instance.EnableSync)
        {
          _gamePersistenceProvider.Persistence = PersistencePreferences.instance.GamePersistence;
          PersistencePreferences.instance.GamePersistence = _gamePersistenceProvider.Persistence;
        }
      }
      else
      {
        _gamePersistenceProvider.Persistence = null;
      }
    }

    public UniTask SaveAsync()
    {
      if (PersistencePreferences.instance.EnableSaving)
      {
        PersistencePreferences.instance.Save();
        return _gamePersistenceLoader.SaveAsync();
      }
      
      return UniTask.CompletedTask;
    }
  }
}