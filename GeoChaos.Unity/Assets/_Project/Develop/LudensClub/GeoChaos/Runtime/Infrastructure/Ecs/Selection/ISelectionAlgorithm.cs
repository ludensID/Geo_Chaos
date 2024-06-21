namespace LudensClub.GeoChaos.Runtime.Infrastructure.Selection
{
  public interface ISelectionAlgorithm
  {
    void Select(EcsEntities origins, EcsEntities marks);
  }
}