using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Selection
{
  public interface IEcsEntitySelector
  {
    void Select(EcsEntities origins, EcsEntities targets, EcsEntities selections);
  }
}