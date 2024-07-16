using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Converters
{
  public interface IGameObjectConverterService
  {
    void Convert(EcsEntity entity, GameObject gameObject);
    void Convert<TComponent>(EcsEntity entity, TComponent component) where TComponent : Component;
  }
}