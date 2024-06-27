using System.Linq;
using UnityEditor;
using UnityEngine;

namespace LudensClub.GeoChaos.Editor.General
{
  public static class AssetFinder
  {
    public static TAsset FindAsset<TAsset>() where TAsset : Object
    {
      return FindAssets<TAsset>(typeof(TAsset).Name)
        .FirstOrDefault();
    }
    
    public static TAsset FindAsset<TAsset>(string typeName) where TAsset : Object
    {
      return FindAssets<TAsset>(typeName)
        .FirstOrDefault();
    }

    public static TAsset[] FindAssets<TAsset>(string typeName) where TAsset : Object
    {
      return AssetDatabase.FindAssets($"t: {typeName}")
        .Select(AssetDatabase.GUIDToAssetPath)
        .Select(AssetDatabase.LoadAssetAtPath<TAsset>)
        .ToArray();
    }
  }
}