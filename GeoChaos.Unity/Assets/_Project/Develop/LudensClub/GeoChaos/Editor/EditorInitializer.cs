using LudensClub.GeoChaos.Editor.General;
using LudensClub.GeoChaos.Runtime;
using UnityEditor;
using UnityEngine.InputSystem;
using TypeCache = LudensClub.GeoChaos.Editor.General.TypeCache;

namespace LudensClub.GeoChaos.Editor
{
  [InitializeOnLoad]
  public static class EditorInitializer
  {
    static EditorInitializer()
    {
      var container = new EditorContainer();
      EditorContext.Container = container;
      var inputAsset = AssetFinder.FindAsset<InputActionAsset>();
      container.InputAsset = inputAsset;
      container.ProfilerService = new ProfilerService();
      container.TypeCache = new TypeCache();
    }
  }
}