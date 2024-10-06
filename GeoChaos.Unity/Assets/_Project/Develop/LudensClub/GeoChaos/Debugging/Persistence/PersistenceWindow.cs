using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LudensClub.GeoChaos.Debugging.Persistence
{
  public class PersistenceWindow : EditorWindow
  {
    private PersistencePreferencesEditor _editor;
    private bool _recreated;

    [MenuItem("Window/Persistence Window")]
    public static void GetOrCreateWindow()
    {
      var window = GetWindow<PersistenceWindow>();
      window.titleContent = new GUIContent("Persistence");
    }

    private void OnEnable()
    {
      Subscribe();
      RecreateGUI();
    }

    private void Subscribe()
    {
      SceneManager.activeSceneChanged += OnActiveSceneChanged;
      EditorSceneManager.activeSceneChangedInEditMode += OnActiveSceneChanged;
    }

    private void OnActiveSceneChanged(Scene oldScene, Scene newScene)
    {
      RecreateGUI();
    }

    private void Unsubscribe()
    {
      SceneManager.activeSceneChanged -= OnActiveSceneChanged;
      EditorSceneManager.activeSceneChangedInEditMode -= OnActiveSceneChanged;
    }

    private void RecreateGUI()
    {
      if (_editor)
        DestroyImmediate(_editor);

      _editor = (PersistencePreferencesEditor)UnityEditor.Editor.CreateEditor(PersistencePreferences.instance);
      CreateGUI();
    }

    private void CreateGUI()
    {
      rootVisualElement.Clear();
      rootVisualElement.Add(_editor.CreateInspectorGUI());
    }

    private void OnGUI()
    {
      if (EditorUtility.IsDirty(PersistencePreferences.instance))
      {
        PersistencePreferences.instance.Save();
      }
    }

    private void DisposeInternalData()
    {
      if (_editor)
        DestroyImmediate(_editor);
    }

    private void OnDisable()
    {
      DisposeInternalData();
      Unsubscribe();
    }
  }
}