namespace LudensClub.GeoChaos.Runtime.Infrastructure.Selection
{
  public interface IEcsEntitySelector
  {
    void Select<TComponent>(EcsEntities origins, EcsEntities targets, EcsEntities selections) where TComponent : struct, IEcsComponent;
  }
}