using Leopotam.EcsLite;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Converters
{
  public interface IEcsConverter
  {
    public void Convert(EcsWorld world, int entity);
  }
}