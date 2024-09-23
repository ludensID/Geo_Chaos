using LudensClub.GeoChaos.Editor;
using LudensClub.GeoChaos.Runtime.Persistence;
using TriInspector;
using UnityEngine;

namespace LudensClub.GeoChaos.Debugging.Persistence
{
  [HideMonoScript]
  public class PersistencePreferences : ScriptableObject
  {
    public bool EnableSaving;
    public bool EnableSync;
      
    public GameData GameData;

    [Button(ButtonSizes.Medium, MIC.Names.DELETE_GAMEPLAY_SAVINGS)]
    public void DeleteGameplaySavings()
    {
      PersistenceFunctions.DeleteGameplaySavings();
    }

    [Button(ButtonSizes.Medium, MIC.Names.OPEN_PERSISTENCE_FOLDER)]
    public void OpenPersistenceFolder()
    {
      PersistenceFunctions.OpenPersistenceFolder();
    }

    [Button(ButtonSizes.Medium, MIC.Names.OPEN_PERSISTENCE_WINDOW)]
    public void OpenPersistenceWindow()
    {
      PersistenceFunctions.OpenPersistenceWindow();
    }
  }
}