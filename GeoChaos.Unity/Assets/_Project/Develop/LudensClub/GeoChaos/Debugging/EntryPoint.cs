using LudensClub.GeoChaos.Runtime;
using UnityEditor;
using UnityEngine;

namespace LudensClub.GeoChaos.Debugging
{
  public static class EntryPoint
  {
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Entry()
    {
      if (!EditorApplication.isPlaying)
      {
        Debug.Log("EDITOR: Runtime initialize in editor mode");
        return;
      }

      DebugBridge.OnProjectInstalled += DebugInstaller.InstallProject;
      DebugBridge.OnGameplayInstalled += DebugInstaller.InstallGameplay;
    }
  }
}