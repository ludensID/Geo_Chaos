using Cysharp.Threading.Tasks;

namespace LudensClub.GeoChaos.Runtime.Persistence
{
  public class PersistenceService : IPersistenceService
  {
    private readonly IGamePersistenceProcessor _processor;
    private readonly IGameDataProvider _provider;
    private readonly IGameDataLoader _loader;
      
    public GameData Data
    {
      get => _provider.Data;
      set => _provider.Data = value;
    }

    public PersistenceService(IGamePersistenceProcessor processor, IGameDataProvider provider, IGameDataLoader loader)
    {
      _processor = processor;
      _provider = provider;
      _loader = loader;
    }

    public async UniTask LoadAsync()
    {
      await _loader.LoadAsync();
      Data ??= new GameData();
    }

    public GameData GetDirtyData()
    {
      _processor.SetDirty();
      return Data;
    }

    public void Save()
    {
      _processor.SetDirty();
    }
  }
}