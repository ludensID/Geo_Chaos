using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Persistence
{
  public class PathHandler : IPathHandler
  {
    public string GetGameFolder() => Application.persistentDataPath;

    public string GetGameDataPath() => GetGameFolder() + "/GameData.json";
    public string GetSettingsDataPath() => GetGameFolder() + "/Settings.json";
  }
}