namespace LudensClub.GeoChaos.Runtime.Persistence
{
  public interface IPathHandler
  {
    string GameFolder { get; }
    string GameDataPath { get; }
    string SettingsDataPath { get; }
  }
}