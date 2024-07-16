namespace LudensClub.GeoChaos.Runtime.Infrastructure.Converters
{
  public interface IGameObjectConverter : IEcsConverter
  {
    bool ShouldCreateEntity { get; set; }
    void CreateEntity(EcsEntity entity);
    void ConvertBackAndDestroy(EcsEntity entity);
  }
}