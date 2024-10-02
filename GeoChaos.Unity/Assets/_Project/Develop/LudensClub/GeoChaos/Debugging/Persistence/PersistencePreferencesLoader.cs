using System;
using LudensClub.GeoChaos.Editor.General;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace LudensClub.GeoChaos.Debugging.Persistence
{
  public class PersistencePreferencesLoader : IEditorInitializable, IPersistencePreferencesLoader
  {
    private const string PERSISTENCE_PREFERENCES_KEY = "PERSISTENCE_PREFERENCES";
      
    private readonly IPersistencePreferencesProvider _persistence;

    public PersistencePreferencesLoader(IPersistencePreferencesProvider persistence)
    {
      _persistence = persistence;
    }
      
    public void Initialize()
    {
      _persistence.Preferences = ScriptableObject.CreateInstance<PersistencePreferences>();
      if (!LoadFromJson())
        SaveToJson();
    }
    
    public bool LoadFromJson()
    {
      try
      {
        if (EditorPrefs.HasKey(PERSISTENCE_PREFERENCES_KEY))
        {
          string value = EditorPrefs.GetString(PERSISTENCE_PREFERENCES_KEY, "");
          EditorJsonUtility.FromJsonOverwrite(value, _persistence.Preferences);

          return true;
        }

        return false;
      }
      catch (Exception e)
      {
        Debug.Log(e);
        return false;
      }
    }
    
    public void SaveToJson()
    {
      string value = EditorJsonUtility.ToJson(_persistence.Preferences);
      EditorPrefs.SetString(PERSISTENCE_PREFERENCES_KEY, value);
    }
  }
}