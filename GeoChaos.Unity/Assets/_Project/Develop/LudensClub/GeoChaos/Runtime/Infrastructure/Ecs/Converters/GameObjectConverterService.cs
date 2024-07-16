using System.Collections.Generic;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Converters
{
  public class GameObjectConverterService : IGameObjectConverterService
  {
    public void Convert(EcsEntity entity, GameObject gameObject)
    {
      var converters = new List<IEcsConverter>();
      GameObjectConverter.GetConverters(gameObject.transform, converters);
      foreach (IEcsConverter converter in converters) 
        converter.ConvertTo(entity);
    }

    public void Convert<TComponent>(EcsEntity entity, TComponent component) where TComponent : Component
    {
      Convert(entity, component.gameObject);
    }
  }
}