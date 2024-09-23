using TriInspector.Editors;
using UnityEditor;
using UnityEngine.UIElements;

namespace LudensClub.GeoChaos.Debugging.Persistence
{
  [CustomEditor(typeof(PersistencePreferences), true)]
  public class PersistencePreferencesEditor : UnityEditor.Editor
  {
    private TriEditorCore _core;
    private bool _initialized;

    private void OnEnable()
    {
      _core = new TriEditorCore(this);
      _initialized = false;
    }

    private void Initialize()
    {
      _initialized = true;
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
      if (!_initialized)
        Initialize();
    }
  }
}