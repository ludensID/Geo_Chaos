using Cysharp.Threading.Tasks;

namespace LudensClub.GeoChaos.Runtime.Persistence
{
  public abstract class PersistenceDataLoader<TData> : IPersistenceDataLoader where TData : class, IPersistenceData
  {
    private readonly IPersistenceDataProvider<TData> _persistencePersistenceProvider;
    private readonly IFileHandler _fileHandler;
    private readonly string _path;

    public PersistenceDataLoader(IPersistenceDataProvider<TData> persistencePersistenceProvider,
      IFileHandler fileHandler,
      string path)
    {
      _persistencePersistenceProvider = persistencePersistenceProvider;
      _fileHandler = fileHandler;
      _path = path;
    }

    public async UniTask LoadAsync()
    {
      _persistencePersistenceProvider.Persistence = await _fileHandler.LoadAsync<TData>(_path);
    }

    public async UniTask SaveAsync()
    {
      await _fileHandler.SaveAsync(_path, _persistencePersistenceProvider.Persistence);
    }
  }
}