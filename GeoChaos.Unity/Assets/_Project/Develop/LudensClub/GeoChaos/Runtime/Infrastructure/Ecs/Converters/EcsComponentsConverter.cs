using System;
using System.Collections.Generic;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Utils;
using TriInspector;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Converters
{
  [Serializable]
  public class EcsComponentsConverter : IEcsConverter
  {
    public List<EcsComponentValue> Components;

    public void ConvertTo(EcsEntity entity)
    {
      EcsWorld world = entity.World;
      foreach (EcsComponentValue componentValue in Components)
      {
        IEcsPool pool = world.GetPoolEnsure(componentValue.Type);
        pool.AddRaw(entity.Entity, componentValue.Value);
      }
    }

    public void ConvertBack(EcsEntity entity)
    {
    }

#if UNITY_EDITOR
    [Button("Clear")]
    [GUIColor(1f, 0f, 0)]
    [PropertyOrder(0)]
    private void Clear()
    {
      Components.Clear();
    }
#endif
  }
}