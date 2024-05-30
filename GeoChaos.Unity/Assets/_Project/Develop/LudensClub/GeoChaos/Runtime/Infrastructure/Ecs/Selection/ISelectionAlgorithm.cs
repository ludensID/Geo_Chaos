using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Selection
{
  public interface ISelectionAlgorithm
  {
    void Select(EcsEntities origins, EcsEntities selections);
  }
}