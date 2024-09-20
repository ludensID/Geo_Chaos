using System.Threading.Tasks;

namespace LudensClub.GeoChaos.Runtime.Persistence
{
  public interface IFileHandler
  {
    Task<TData> LoadAsync<TData>(string filePath) where TData : class;
    Task SaveAsync<TData>(string filePath, TData data) where TData : class;
  }
}