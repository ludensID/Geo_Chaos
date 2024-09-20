namespace LudensClub.GeoChaos.Runtime.Persistence
{
  public interface IPathHandler
  {
    string GetGameFolder();
    string GetGameDataPath();
    string GetSettingsDataPath();
  }
}