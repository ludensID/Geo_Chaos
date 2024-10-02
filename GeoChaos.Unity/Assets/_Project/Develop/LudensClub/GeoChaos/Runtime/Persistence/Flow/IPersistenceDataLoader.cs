using Cysharp.Threading.Tasks;

namespace LudensClub.GeoChaos.Runtime.Persistence
{
  public interface IPersistenceDataLoader
  {
    UniTask LoadAsync();
    UniTask SaveAsync();
  }
}