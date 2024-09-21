using Cysharp.Threading.Tasks;

namespace LudensClub.GeoChaos.Runtime.Persistence
{
  public interface IPersistenceService
  {
    GameData Data { get; set; }
    UniTask LoadAsync();
    GameData GetDirtyData();
    void Save();
  }
}