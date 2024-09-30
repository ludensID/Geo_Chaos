using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Persistence
{
  public class PathHandler : IPathHandler
  {
    public string GameFolder { get; }

    public string GameDataPath { get; }

    public string SettingsDataPath { get; }

    public PathHandler()
    {
      GameFolder = Application.persistentDataPath + "/Persistence";
      GameDataPath = GameFolder + "/GameData.json";
      SettingsDataPath = GameFolder + "/Settings.json";
    }
  }
}