using LudensClub.GeoChaos.Editor.General;
using LudensClub.GeoChaos.Runtime;
using LudensClub.GeoChaos.Runtime.Configuration;
using UnityEditor;
using UnityEngine.InputSystem;

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
      var nameMap = AssetFinder.FindAsset<InputActionNameMap>();
      container.InputAsset = inputAsset;
      container.NameMap = nameMap;
      container.ProfilerService = new ProfilerService();
        
      InputMapUpdater.Construct();
    }
  }
}