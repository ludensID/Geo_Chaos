using System;
using System.IO;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Persistence
{
  public class FileHandler : IFileHandler
  {
    public async UniTask<TData> LoadAsync<TData>(string filePath) where TData : class
    {
      try
      {
        if (!File.Exists(filePath))
          return null;

        return JsonConvert.DeserializeObject<TData>(await File.ReadAllTextAsync(filePath));
      }
      catch (Exception e)
      {
        Debug.LogException(e);
        return null;
      }
    }

    public async UniTask SaveAsync<TData>(string filePath, TData data) where TData : class
    {
      try
      {
        string json = JsonConvert.SerializeObject(data);

        Directory.CreateDirectory(Path.GetDirectoryName(filePath) ?? throw new ArgumentException());
        await File.WriteAllTextAsync(filePath, json);
      }
      catch (Exception e)
      {
        Debug.LogException(e);
      }
    }
  }
}