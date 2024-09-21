using Cysharp.Threading.Tasks;

namespace LudensClub.GeoChaos.Runtime.Persistence
{
  public interface IGameDataLoader
  {
    UniTask LoadAsync();
    UniTask SaveAsync();
  }
}