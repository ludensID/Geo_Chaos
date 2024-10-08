using LudensClub.GeoChaos.Editor.General;
using LudensClub.GeoChaos.Runtime.Boot;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Infrastructure.SceneLoading;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LudensClub.GeoChaos.Editor.Boot
{
  public class PlayModeSceneLoader
  {
    private static string _currentScenePath;
    public static SceneType CurrentSceneType;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Load()
    {
      _currentScenePath = SceneManager.GetActiveScene().path;
      CurrentSceneType = DetermineSceneType();
      if (CurrentSceneType == SceneType.Boot)
        return;

      var configProvider = AssetFinder.FindAsset<ConfigProvider>(nameof(ConfigProvider));
      string path = configProvider.Get<SceneConfig>().GetSceneName(SceneType.Boot);
      EditorSceneManager.LoadSceneInPlayMode(path, new LoadSceneParameters());
    }

    public static Scene LoadCurrentScene()
    {
      return EditorSceneManager.LoadSceneInPlayMode(_currentScenePath, new LoadSceneParameters());
    }

    private static SceneType DetermineSceneType()
    {
      if (HasObject<BootInstaller>())
        return SceneType.Boot;

      if (HasObject<MenuInstaller>())
        return SceneType.Menu;

      if (HasObject<LevelInstaller>())
        return SceneType.Game;

      return SceneType.None;
    }

    private static bool HasObject<TObject>() where TObject : Object
    {
      return Object.FindAnyObjectByType<TObject>(FindObjectsInactive.Include);
    }
  }
}