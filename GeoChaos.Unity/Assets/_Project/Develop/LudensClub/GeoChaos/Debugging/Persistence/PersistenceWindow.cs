using LudensClub.GeoChaos.Editor.General;
using LudensClub.GeoChaos.Runtime;
using TriInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace LudensClub.GeoChaos.Debugging.Persistence
{
  [InlineEditor]
  public class PersistenceWindow : EditorWindow, IEditorInitializable
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
      Inject();
      Subscribe();
    }

    private void Inject()
    {
      EditorMediator.Context.Container.Inject(this);
    }

    private void Subscribe()
    {
      EditorMediator.Context.AddListener(Reinject);
    }

    private void Unsubscribe()
    {
      EditorMediator.Context.RemoveListener(Reinject);
    }

    private void Reinject()
    {
      DisposeInternalData();
      Inject();
    }

    [Inject]
    public void Construct(IPersistencePreferencesLoader persistenceLoader,
      IPersistencePreferencesProvider persistenceProvider, 
      IEditorInitializer initializer)
    {
      _persistenceProvider = persistenceProvider;
      _persistenceLoader = persistenceLoader;
      initializer.Add(this);
    }

    public void Initialize()
    {
      Recreate();
    }

    private bool CheckState()
    {
      return _editor && _editor.target;
    }

    private void Recreate()
    {
      if (_editor)
        DestroyImmediate(_editor);

      _editor = (PersistencePreferencesEditor)UnityEditor.Editor.CreateEditor(_persistenceProvider.Preferences);
      CreateGUI();
    }

    private void CreateGUI()
    {
      rootVisualElement.Clear();
      rootVisualElement.Add(new IMGUIContainer(CheckGUI));
      rootVisualElement.Add(_editor.CreateInspectorGUI());
    }

    private void CheckGUI()
    {
      if (!CheckState())
      {
      }
    }

    private void OnGUI()
    {
      if (EditorUtility.IsDirty(_persistenceProvider.Preferences))
      {
        _persistenceLoader.SaveToJson();
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