using Leopotam.EcsLite;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Converters
{
  public class GameObjectConverter : IGameObjectConverter
  {
    public void Convert(EcsWorld world, int entity, GameObject gameObject)
    {
      foreach (var converter in gameObject.GetComponents<IEcsConverter>()) converter.Convert(world, entity);
    }

    public void Convert<TComponent>(EcsWorld world, int entity, TComponent component) where TComponent : Component
    {
      Convert(world, entity, component.gameObject);
    }
  }
}