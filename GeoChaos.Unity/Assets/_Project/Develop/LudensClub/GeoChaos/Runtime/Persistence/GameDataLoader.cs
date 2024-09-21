using Cysharp.Threading.Tasks;

namespace LudensClub.GeoChaos.Runtime.Persistence
{
  public class GameDataLoader : IGameDataLoader
  {
    private readonly IGameDataProvider _gameDataProvider;
    private readonly IPathHandler _pathHandler;
    private readonly IFileHandler _fileHandler;

    public GameDataLoader(IGameDataProvider gameDataProvider, IPathHandler pathHandler, IFileHandler fileHandler)
    {
      _gameDataProvider = gameDataProvider;
      _pathHandler = pathHandler;
      _fileHandler = fileHandler;
    }

    public async UniTask LoadAsync()
    {
      _gameDataProvider.Data = await _fileHandler.LoadAsync<GameData>(_pathHandler.GetGameDataPath());
    }

    public async UniTask SaveAsync()
    {
      await _fileHandler.SaveAsync(_pathHandler.GetGameDataPath(), _gameDataProvider.Data);
    }
  }
}