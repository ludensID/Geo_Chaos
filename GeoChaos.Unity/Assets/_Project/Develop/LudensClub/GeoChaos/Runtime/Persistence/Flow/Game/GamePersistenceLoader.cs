namespace LudensClub.GeoChaos.Runtime.Persistence
{
  public class GamePersistenceLoader : PersistenceDataLoader<GamePersistence>, IGamePersistenceLoader
  {
    public GamePersistenceLoader(IGamePersistenceProvider gamePersistenceProvider, IPathHandler pathHandler, IFileHandler fileHandler)
    : base(gamePersistenceProvider, fileHandler, pathHandler.GameDataPath)
    {
    }
  }
}