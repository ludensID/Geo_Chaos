using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace LudensClub.GeoChaos.Editor.General
{
  public static class AssetFinder
  {
    public static TAsset FindAsset<TAsset>() where TAsset : Object
    {
      return FindAsset<TAsset>(typeof(TAsset).Name);
    }
    
    public static TAsset FindAsset<TAsset>(string typeName) where TAsset : Object
    {
      return FindAssets<TAsset>(typeName)
        .FirstOrDefault();
    }
    
    public static TAsset FindAsset<TAsset>(string typeName, string assetName) where TAsset : Object
    {
      return FindAssets<TAsset>(typeName, assetName)
        .FirstOrDefault();
    }

    public static TAsset[] FindAssets<TAsset>(string typeName) where TAsset : Object
    {
      return AssetDatabase.FindAssets($"t: {typeName}")
        .SelectAssets<TAsset>()
        .ToArray();
    }
    
    public static TAsset[] FindAssets<TAsset>(string typeName, string assetName) where TAsset : Object
    {
      return AssetDatabase.FindAssets($"t: {typeName} {assetName}")
        .SelectAssets<TAsset>()
        .ToArray();
    }

    private static IEnumerable<TAsset> SelectAssets<TAsset>(this IEnumerable<string> guids) where TAsset : Object
    {
      return guids
        .Select(AssetDatabase.GUIDToAssetPath)
        .Select(AssetDatabase.LoadAssetAtPath<TAsset>);
    }
  }
}