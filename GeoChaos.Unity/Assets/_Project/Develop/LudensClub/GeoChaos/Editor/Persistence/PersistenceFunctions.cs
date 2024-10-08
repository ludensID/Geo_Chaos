using System.Diagnostics;
using System.IO;
using LudensClub.GeoChaos.Runtime.Persistence;
using UnityEditor;
using Debug = UnityEngine.Debug;

namespace LudensClub.GeoChaos.Editor.Persistence
{
  public static class PersistenceFunctions
  {
    [MenuItem(MIC.Names.DELETE_GAMEPLAY_SAVINGS_MENU)]
    public static void DeleteGameplaySavings()
    {
      var pathHandler = new PathHandler();
      if (File.Exists(pathHandler.GameDataPath))
      {
        File.Delete(pathHandler.GameDataPath);
        Debug.Log("Gameplay Savings was successfully deleted");
      }
    }
    
    [MenuItem(MIC.Names.OPEN_PERSISTENCE_FOLDER_MENU)]
    public static void OpenPersistenceFolder()
    {
      var pathHandler = new PathHandler();
      if (Directory.Exists(pathHandler.GameFolder))
      {
        Process.Start(pathHandler.GameFolder);
      }
      else
      {
        Debug.Log("Persistence Folder does not exist");
      }
    }
      
    [MenuItem(MIC.Names.OPEN_PERSISTENCE_WINDOW_MENU)]
    public static void OpenPersistenceWindow()
    {
      PersistenceWindow.GetOrCreateWindow();
    }
  }
}