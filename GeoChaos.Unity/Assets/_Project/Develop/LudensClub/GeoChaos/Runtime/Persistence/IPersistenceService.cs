using System.Threading.Tasks;

namespace LudensClub.GeoChaos.Runtime.Persistence
{
  public interface IPersistenceService
  {
    GameData Data { get; set; }
    Task LoadAsync();
    void Save();
  }
}