using LudensClub.GeoChaos.Runtime;
using UnityEngine;

namespace LudensClub.GeoChaos.Editor.Boot
{
  public static class PlayModeEntryPoint
  {
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Entry()
    {
      DebugBridge.OnProjectInstalled += DebugProjectInstaller.Install;
      DebugBridge.OnBootInstalled += DebugBootInstaller.Install;
      DebugBridge.OnGameplayInstalled += DebugLevelInstaller.Install;
    }
  }
}