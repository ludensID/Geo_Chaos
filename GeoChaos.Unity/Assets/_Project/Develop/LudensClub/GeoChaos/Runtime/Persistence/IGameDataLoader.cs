using System.Threading.Tasks;

namespace LudensClub.GeoChaos.Runtime.Persistence
{
  public interface IGameDataLoader
  {
    Task LoadAsync();
    Task SaveAsync();
  }
}