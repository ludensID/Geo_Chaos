using Cysharp.Threading.Tasks;

namespace LudensClub.GeoChaos.Runtime.Persistence
{
  public interface IGamePersistenceProcessor
  {
    void SetDirty();
    UniTask SaveDirectAsync();
  }
}