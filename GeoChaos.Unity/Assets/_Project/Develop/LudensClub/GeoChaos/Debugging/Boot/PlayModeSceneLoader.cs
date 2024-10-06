using LudensClub.GeoChaos.Editor.General;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LudensClub.GeoChaos.Debugging.Boot
{
  public class PlayModeSceneLoader
  {
    private static string _currentScenePath;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
    private static void Load()
    {
      _currentScenePath = SceneManager.GetActiveScene().path;
      var editorScene = AssetFinder.FindAsset<SceneAsset>(nameof(SceneAsset), "EditorBoot");
      string path = AssetDatabase.GetAssetPath(editorScene);
      EditorSceneManager.LoadSceneInPlayMode(path, new LoadSceneParameters());
    }

    public static void LoadCurrentScene()
    {
      EditorSceneManager.LoadSceneInPlayMode(_currentScenePath, new LoadSceneParameters());
    }
    
    
  }
}