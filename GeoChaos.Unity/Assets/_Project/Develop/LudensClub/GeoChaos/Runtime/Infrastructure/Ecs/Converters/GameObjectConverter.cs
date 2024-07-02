using System.Collections.Generic;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Converters
{
  public class GameObjectConverter : IGameObjectConverter
  {
    public void Convert(EcsEntity entity, GameObject gameObject)
    {
      var converters = new List<IEcsConverter>();
      MonoGameObjectConverter.GetConverters(gameObject.transform, converters);
      foreach (IEcsConverter converter in converters) 
        converter.Convert(entity);
    }

    public void Convert<TComponent>(EcsEntity entity, TComponent component) where TComponent : Component
    {
      Convert(entity, component.gameObject);
    }
  }
}