using TriInspector.Editors;
using UnityEditor;
using UnityEngine.UIElements;

namespace LudensClub.GeoChaos.Editor.Persistence
{
  [CustomEditor(typeof(PersistencePreferences), true)]
  public class PersistencePreferencesEditor : UnityEditor.Editor
  {
    private TriEditorCore _core;
    public bool Initialized;

    private void OnEnable()
    {
      _core = new TriEditorCore(this);
      Initialized = false;
    }

    private void Initialize()
    {
      Initialized = true;
    }

    private void OnDisable()
    {
      _core?.Dispose();
    }

    public override void OnInspectorGUI()
    {
      UpdateGUI();
      _core.OnInspectorGUI();
    }

    public override VisualElement CreateInspectorGUI()
    {
      var container = new VisualElement();
      container.Add(new IMGUIContainer(DrawHeader));
      container.Add(new IMGUIContainer(UpdateGUI));
      
      VisualElement coreElement = _core.CreateVisualElement();
      coreElement.style.marginTop = 3;
      coreElement.style.marginLeft = 15;
      coreElement.style.marginRight = 6;
      container.Add(coreElement);

      return container;
    }

    private void UpdateGUI()
    {
      if (!Initialized)
        Initialize();
    }
  }
}