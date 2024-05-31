using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Converters
{
  public class GameObjectConverter : IGameObjectConverter
  {
    public void Convert(EcsEntity entity, GameObject gameObject)
    {
      foreach (IEcsConverter converter in gameObject.GetComponents<IEcsConverter>()) 
        converter.Convert(entity);
    }

    public void Convert<TComponent>(EcsEntity entity, TComponent component) where TComponent : Component
    {
      Convert(entity, component.gameObject);
    }
  }
}