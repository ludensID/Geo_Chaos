using LudensClub.GeoChaos.Runtime;
using TriInspector;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Debugging.Persistence
{
  [InlineEditor]
  public class PersistenceWindow : EditorWindow
  {
    private PersistencePreferencesEditor _editor;
    private IPersistencePreferencesLoader _persistenceLoader;
    private IPersistencePreferencesProvider _persistenceProvider;

    [MenuItem("Window/Persistence Window")]
    public static void GetOrCreateWindow()
    {
      var window = GetWindow<PersistenceWindow>();
      window.titleContent = new GUIContent("Persistence");
    }

    private void OnEnable()
    {
      EditorMediator.Context.Container.Inject(this);
    }

    [Inject]
    public void Construct(IPersistencePreferencesLoader persistenceLoader,
      IPersistencePreferencesProvider persistenceProvider)
    {
      _persistenceProvider = persistenceProvider;
      _persistenceLoader = persistenceLoader;
      _editor = (PersistencePreferencesEditor) UnityEditor.Editor.CreateEditor(_persistenceProvider.Preferences);
    }

    private void CreateGUI()
    {
      rootVisualElement.Add(_editor.CreateInspectorGUI());
    }

    private void OnGUI()
    {
      if (EditorUtility.IsDirty(_persistenceProvider.Preferences))
      {
        _persistenceLoader.SaveToJson();
      }
    }

    private void OnDisable()
    {
      DestroyImmediate(_editor);
    }
  }
}