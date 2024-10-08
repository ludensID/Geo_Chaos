using LudensClub.GeoChaos.Runtime.Persistence;
using TriInspector;
using UnityEditor;

namespace LudensClub.GeoChaos.Editor.Persistence
{
  [HideMonoScript]
  [FilePath("UserSettings/GeoChaos/PersistencePreferences.asset", FilePathAttribute.Location.ProjectFolder)]
  public class PersistencePreferences : ScriptableSingleton<PersistencePreferences>
  {
    public bool EnableSaving;
    public bool EnableSync;
      
    public GamePersistence GamePersistence;

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
  }
}