using Cysharp.Threading.Tasks;

namespace LudensClub.GeoChaos.Runtime.Persistence
{
  public interface IFileHandler
  {
    UniTask<TData> LoadAsync<TData>(string filePath) where TData : class;
    UniTask SaveAsync<TData>(string filePath, TData data) where TData : class;
  }
}