namespace LudensClub.GeoChaos.Runtime.Infrastructure.Converters
{
  public interface IEcsConverter
  {
    public void ConvertTo(EcsEntity entity);
    public void ConvertBack(EcsEntity entity);
  }
}