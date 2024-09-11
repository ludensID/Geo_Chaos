#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System;

namespace GitIntegration
{
  [InitializeOnLoad]
  public class SmartMergeRegistrar
  {
    private const string SMART_MERGE_REGISTRAR_EDITOR_PREFS_KEY = "smart_merge_installed";
    private const int VERSION = 1;
    private static readonly string _versionKey = $"{VERSION}_{Application.unityVersion}";

    [MenuItem("Tools/Git/SmartMerge registration")]
    private static void SmartMergeRegister()
    {
      try
      {
        string unityYamlMergePath = EditorApplication.applicationContentsPath + "/Tools" + "/UnityYAMLMerge.exe";
        Utils.ExecuteGitWithParams("config merge.unityyamlmerge.name \"Unity SmartMerge (UnityYamlMerge)\"");
        Utils.ExecuteGitWithParams(
          $"config merge.unityyamlmerge.driver \"\\\"{unityYamlMergePath}\\\" merge -h -p --force --fallback none %O %B %A %A\"");
        Utils.ExecuteGitWithParams("config merge.unityyamlmerge.recursive binary");
        EditorPrefs.SetString(SMART_MERGE_REGISTRAR_EDITOR_PREFS_KEY, _versionKey);
        Debug.Log($"Successfully registered UnityYAMLMerge with path {unityYamlMergePath}");
      }
      catch (Exception e)
      {
        Debug.Log($"Fail to register UnityYAMLMerge with error: {e}");
      }
    }

    [MenuItem("Tools/Git/SmartMerge unregistration")]
    private static void SmartMergeUnregister()
    {
      Utils.ExecuteGitWithParams("config --remove-section merge.unityyamlmerge");
      Debug.Log("Successfully unregistered UnityYAMLMerge");
    }

    static SmartMergeRegistrar()
    {
      string installedVersionKey = EditorPrefs.GetString(SMART_MERGE_REGISTRAR_EDITOR_PREFS_KEY);
      if (installedVersionKey != _versionKey)
        SmartMergeRegister();
    }
  }
}
#endif