using UnityEditor;
using UnityEngine;

namespace LudensClub.GeoChaos.Debugging
{
  public static class Misc
  {
    public static T FindAsset<T>(string typeName) where T : Object
    {
      string[] asset = AssetDatabase.FindAssets($"t:{typeName}", null);
      return asset.Length == 1
        ? AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(asset[0]))
        : default;
    }
  }
}