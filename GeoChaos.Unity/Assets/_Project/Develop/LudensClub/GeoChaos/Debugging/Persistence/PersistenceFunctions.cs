using System.Diagnostics;
using System.IO;
using LudensClub.GeoChaos.Editor;
using LudensClub.GeoChaos.Runtime.Persistence;
using UnityEditor;
using Debug = UnityEngine.Debug;

namespace LudensClub.GeoChaos.Debugging.Persistence
{
  public static class PersistenceFunctions
  {
    [MenuItem(MIC.Names.DELETE_GAMEPLAY_SAVINGS_MENU)]
    public static void DeleteGameplaySavings()
    {
      var pathHandler = new PathHandler();
      if (File.Exists(pathHandler.GetGameDataPath()))
      {
        File.Delete(pathHandler.GetGameDataPath());
        Debug.Log("Gameplay Savings was successfully deleted");
      }
    }
    
    [MenuItem(MIC.Names.OPEN_PERSISTENCE_FOLDER_MENU)]
    public static void OpenPersistenceFolder()
    {
      var pathHandler = new PathHandler();
      if (Directory.Exists(pathHandler.GetGameFolder()))
      {
        Process.Start(pathHandler.GetGameFolder());
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